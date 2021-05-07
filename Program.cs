using System.IO;

// https://en.wikipedia.org/wiki/Ls

namespace lsd
{
    static class Program
    {
        static bool ShowDirectories = false;
        static bool ShowFiles = false;
        static bool ShowAsList = false;

        static int Main(string[] args)
        {
            ParseArgs(args);

            DirectoryManager.ListAllDirectoryItems(
                Directory.GetCurrentDirectory(),
                ShowDirectories,
                ShowFiles,
                ShowAsList);

            return 0;
        }

        static void ParseArgs(string[] args)
        {
            // Show all sorted directory items in one straight row. Default for 'ls'
            if (args.Length == 0)
            {
                ShowDirectories = false;
                ShowFiles = false;
                ShowAsList = false;

                return;
            }

            foreach (var argument in args)
            {
                if (argument.ToLower().Contains("-directories")) // || argument.ToLower().Contains("-d") || argument.ToLower().Contains("d"))
                    ShowDirectories = true;

                if (argument.ToLower().Contains("-files")) // || argument.ToLower().Contains("-f") || argument.ToLower().Contains("f")))
                    ShowFiles = true;

                if (argument.ToLower().Contains("-list")) // || argument.ToLower().Contains("-l") || argument.ToLower().Contains("l"))
                    ShowAsList = true;
                
                // Default behavior in 'ls'
                if (ShowDirectories == false && ShowFiles == false)
                {
                    ShowDirectories = true;
                    ShowFiles = true;
                }
            }
        }
    }
}
