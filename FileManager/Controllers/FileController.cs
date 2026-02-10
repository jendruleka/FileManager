using FileManager.Helpers;
using FileManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
 
namespace FileManager.Controllers
{
    public class FileController : Controller
    {
        private readonly Appsettings _appsettings;
        public FileController(IOptions<Appsettings> options)
        {
            _appsettings = options.Value;
        }

        // GET: /File
        public IActionResult Index()
        {
            return View();
        }

        // POST: /File/Scan
        [HttpPost]
        public IActionResult Scan(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                ViewBag.Error = "Directory doesn't exist.";
                return View("Index");
            }

            var oldState = DirectoryHelper.LoadState(_appsettings.StateFilePath);
            var currentFiles = DirectoryHelper.ScanDirectory(directoryPath);

            var result = new ScanResult();

            // nové a zmìnìné soubory
            foreach (var file in currentFiles)
            {
                if (!oldState.Files.ContainsKey(file.Key))
                {
                    result.NewFiles.Add(file.Key);
                    oldState.Files[file.Key] = new FileRecord
                    {
                        RelativePath = file.Key,
                        Hash = file.Value,
                        Version = 1
                    };
                }
                else if (oldState.Files[file.Key].Hash != file.Value)
                {
                    result.ChangedFiles.Add(file.Key);
                    oldState.Files[file.Key].Hash = file.Value;
                    oldState.Files[file.Key].Version++;
                }
            }

            // odstranìné soubory
            var removed = oldState.Files.Keys.Except(currentFiles.Keys).ToList();
            foreach (var r in removed)
            {
                result.RemovedFiles.Add(r);
                oldState.Files.Remove(r);
            }

            DirectoryHelper.SaveState(oldState, _appsettings.StateFilePath);

            return View("Index", result);
        }

    }
}
