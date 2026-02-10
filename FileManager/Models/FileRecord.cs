namespace FileManager.Models
{
    /// <summary>
    /// Represents a record of a file in the directory scan state.
    /// Stores the file's relative path, its content hash, and the version number.
    /// </summary> 

    public class FileRecord
    {
        /// <summary>
        /// Gets or sets the file's relative path within the scanned directory.
        /// </summary>
        public string RelativePath { get; set; }

        /// <summary>
        /// Gets or sets the hash of the file's contents.
        /// Can be used to detect changes in the file.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the version number of the file record.
        /// </summary>
        public int Version { get; set; }
    }
}
