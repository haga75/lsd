using System.IO;

// https://en.wikipedia.org/wiki/Ls

namespace lsd
{
    static class Program
    {
        static bool ShowOnlyDirectories = false;
        static bool ShowOnlyFiles = false;
        static bool ShowAsList = false;

        static int Main(string[] args)
        {
            ParseArgs(args);

            DirectoryManager.ListAllDirectoryItems(
                Directory.GetCurrentDirectory(),
                ShowOnlyDirectories,
                ShowOnlyFiles,
                ShowAsList);

            return 0;
        }

        static void ParseArgs(string[] args)
        {
            if (args.Length == 0)
            {
                ShowOnlyDirectories = true;
                ShowOnlyFiles = true;
                ShowAsList = false;

                return;
            }

            foreach (var argument in args)
            {
                if (argument.ToLower().Contains("-directories") || argument.ToLower().Contains("d"))
                    ShowOnlyDirectories = true;

                if (argument.ToLower().Contains("-files") || argument.ToLower().Contains("f"))
                    ShowOnlyFiles = true;

                if (argument.ToLower().Contains("-list") || argument.ToLower().Contains("l"))
                    ShowAsList = true;

                // TODO Correct priorities
                if (ShowOnlyDirectories)
                    ShowOnlyFiles = false;
            }
        }
    }
}
