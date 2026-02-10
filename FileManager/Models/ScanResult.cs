namespace FileManager.Models
{
    /// <summary>
    /// Represents the result of scanning a directory for file changes.
    /// Tracks which files are new, which have changed, and which have been removed since the last scan.
    /// </summary>
    public class ScanResult
    {
        /// <summary>
        /// Gets or sets a list of file paths that are new since the last scan.
        /// </summary>
        public List<string> NewFiles { get; set; } = new();

        /// <summary>
        /// Gets or sets a list of file paths that have changed since the last scan.
        /// </summary>
        public List<string> ChangedFiles { get; set; } = new();

        /// <summary>
        /// Gets or sets a list of file paths that have been removed since the last scan.
        /// </summary>
        public List<string> RemovedFiles { get; set; } = new();
    }
}
