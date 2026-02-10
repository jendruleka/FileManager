using FileManager.Models;
using System.Text.Json;

namespace FileManager.Helpers
{
    public class DirectoryHelper
    {
        //private const string StateFilePath = "state.json";

        /// <summary>
        /// Loads the previous directory state from a JSON file.
        /// </summary>
        /// <returns>
        /// A <see cref="DirectoryState"/> containing the last saved state of files.
        /// If the state file does not exist or cannot be deserialized, an empty <see cref="DirectoryState"/> is returned.
        /// </returns>
        public static DirectoryState LoadState(string stateFilePath)
        {
             if (!File.Exists(stateFilePath))
                return new DirectoryState();

             string json = File.ReadAllText(stateFilePath);

             DirectoryState state = JsonSerializer.Deserialize<DirectoryState>(json);

            if (state == null)
                state = new DirectoryState();

            return state;
        }

        /// <summary>
        /// Computes the SHA-256 hash of the given file's contents.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>The SHA-256 hash of the file as a Base64 string.</returns>
        public static void SaveState(DirectoryState state, string stateFilePath)
        {
             string json = JsonSerializer.Serialize(state);

             File.WriteAllText(stateFilePath, json);
        }

        /// <summary>
        /// Scans the given directory recursively and returns a dictionary of files and their content hashes.
        /// </summary>
        /// <param name="path">The root directory to scan.</param>
        /// <returns>
        /// A dictionary where the key is the file's relative path (from <paramref name="path"/>),
        /// and the value is the SHA-256 hash of the file's contents as a Base64 string.
        /// </returns>
        public static Dictionary<string, string> ScanDirectory(string path)
        {
             var fileHashes = new Dictionary<string, string>();

             var allFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

             foreach (var file in allFiles)
            {
                 var hash = FileHelper.ComputeHash(file);

                 var relativePath = Path.GetRelativePath(path, file);

                 fileHashes[relativePath] = hash;
            }

            return fileHashes;
        }
    }
}