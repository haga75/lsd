using System.IO;

// https://en.wikipedia.org/wiki/Ls

namespace lsd
{
    static class Program
    {
        static bool ShowDirectories = false;
        static bool ShowFiles = false;
        static bool ShowAsList = false;
        static bool ShowLength = false;

        static int Main(string[] args)
        {
            ParseArgs(args);

            DirectoryManager.ListDirectoryItems(
                Directory.GetCurrentDirectory(),
                ShowDirectories,
                ShowFiles,
                ShowAsList,
                ShowLength);

            return 0;
        }

        static int ParseArgs(string[] args)
        {
            // Show all sorted directory items in one straight row. Default for 'ls'
            if (args.Length == 0)
            {
                ShowDirectories = true;
                ShowFiles = true;
                ShowAsList = false;
                ShowLength = false;

                return 0;
            }

            foreach (var argument in args)
            {
                if (argument.ToLower().Contains("-directories"))
                    ShowDirectories = true;

                if (argument.ToLower().Contains("-files"))
                    ShowFiles = true;

                if (argument.ToLower().Contains("-list"))
                    ShowAsList = true;

                if (argument.ToLower().Contains("-length"))
                    ShowLength = true;

                // Default behaviour in 'ls'
                if (ShowDirectories == false && ShowFiles == false)
                {
                    ShowDirectories = true;
                    ShowFiles = true;
                }
            }

            return 0;
        }
    }
}
