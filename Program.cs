using System;
using System.IO;
using System.Reflection;

// https://en.wikipedia.org/wiki/Ls

namespace lsd
{
    static class Program
    {
        static bool ShowDirectories = false;
        static bool ShowFiles = false;
        static bool ShowAsList = false;
        static bool ShowLength = false;
        // TODO ShowBatch
        static bool ShowBatch = false;

        static int Main(string[] args)
        {
            ParseArgs(args);

            DirectoryManager.ShowDirectoryItems(
                Directory.GetCurrentDirectory(),
                ShowDirectories,
                ShowFiles,
                ShowAsList,
                ShowLength,
                ShowBatch);

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
                ShowBatch = false;

                return 0;
            }

            if (args.Contains("-version"))
            {
                Console.WriteLine(Assembly.GetEntryAssembly().GetName().Version.ToString());

                return 0;
            }

            string arguments = string.Empty;

            foreach (var argument in args)
                arguments += argument.ToLower() + " ";

            if (arguments.Contains("-directories"))
                ShowDirectories = true;

            if (arguments.Contains("-files"))
                ShowFiles = true;

            if (arguments.Contains("-list"))
                ShowAsList = true;

            if (arguments.Contains("-length"))
                ShowLength = true;

            if (arguments.ToLower().Contains("-batch"))
                ShowBatch = true;

            // Default behaviour in 'ls', show all
            if (ShowDirectories == false && ShowFiles == false)
            {
                ShowDirectories = true;
                ShowFiles = true;
            }

            return 0;
        }

        static bool Contains(this string[] strings, string  text)
        {
            foreach(string s in strings)
            {
                if (s.ToLower().Contains(text))
                    return true;
            }

            return false;
        }
    }
}
