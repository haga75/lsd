using System;
using System.IO;

namespace lsd
{
    class DirectoryItem
    {
        public DirectoryItem(bool isDirectory, string path)
        {
            IsDirectory = isDirectory;
            Path = path;
        }

        public bool IsDirectory { get; }
        public string Path { get; }
        public string Name
        {
            get
            {
                string lastSeparator = (Path.LastIndexOf("/") > 0) ? "/" : "\\";    // Linux or Windows?
                return Path.Substring(Path.LastIndexOf(lastSeparator) + 1);
            }
        }
        public ConsoleColor Color
        {
           get => (IsDirectory)? ConsoleColor.Blue : ConsoleColor.Green;
        }

        /// <summary>
        ///  TODO I have to read the whole file?
        /// </summary>
        public int Size
        {
            get => (IsDirectory) ? 0 : File.ReadAllBytes(Path).Length;
        }
    }
}
