using System;

namespace lsd
{
    public class DirectoryItem
    {
        public bool IsDirectory { get; }
        public string FullName { get; }

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

        public ConsoleColor Color
        {
            get => (IsDirectory) ? ConsoleColor.Blue : ConsoleColor.Green;
        }
        
        public long Length { get; }
    }
}
