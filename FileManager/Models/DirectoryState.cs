namespace FileManager.Models
{
    /// <summary>
    /// Represents the state of a directory at a given point in time.
    /// Stores a collection of files and their corresponding file records.
    /// </summary>
    public class DirectoryState
    {
        /// <summary>
        /// Gets or sets a dictionary of files in the directory.
        /// The key is the file's relative path, and the value is a <see cref="FileRecord"/>
        /// containing metadata such as hash and version.
        /// </summary>
        public Dictionary<string, FileRecord> Files { get; set; } = new();
    }
}
