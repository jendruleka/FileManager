using FileManager.Models;
using System.Security.Cryptography;
using System.Text.Json;

namespace FileManager.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// Saves the current directory state to a JSON file.
        /// </summary>
        /// <param name="state">The <see cref="DirectoryState"/> to save.</param>
        public static string ComputeHash(string filePath)
        {
            using var sha = SHA256.Create();
            using var stream = System.IO.File.OpenRead(filePath);
            return Convert.ToBase64String(sha.ComputeHash(stream));
        }
    }
}
