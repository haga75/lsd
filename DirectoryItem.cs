using System;

namespace lsd
{
    /// <summary>
    /// Represents a directory item. Directory or file.
    /// </summary>
    public class DirectoryItem
    {
        public DirectoryItem(bool isDirectory, string fullName, long length)
        {
            IsDirectory = isDirectory;
            FullName = fullName;
            Length = length;
        }
        
        public bool IsDirectory { get; }
        public string FullName { get; }

        public string Name
        {
            get
            {
                string lastSeparator = (FullName.LastIndexOf("/") > 0) ? "/" : "\\";    // macOS, linux or Windows?
                return FullName[(FullName.LastIndexOf(lastSeparator) + 1)..];
            }
        }

        // TODO Do something with color (LSD = Rainbow?). Calculate contrast colors?
        public ConsoleColor Color
        {
            get => (IsDirectory) ? ConsoleColor.Blue : ConsoleColor.Yellow; // Swedish colors
        }
        
        public long Length { get; }
    }
}
