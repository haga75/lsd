using System;
using System.IO;

// https://en.wikipedia.org/wiki/Ls

namespace lsd
{
    static class Program
    {
        static string DirectoryPath = "";
        static bool DoListDirectories = false;
        static bool DoListFiles = false;
        static bool DoListFileSizes = false;
        static int Main(string[] args)
        {
            ParseArgs(args);

            DirectoryPath = Directory.GetCurrentDirectory();

            if (DoListDirectories)
                ListDirectories(DirectoryPath);

            if (DoListFiles)
                ListFiles(DirectoryPath);

            return 0;
        }

        static int ParseArgs(string[] args)
        {
            if (args.Length == 0)
            { 
                DoListDirectories = true;
                DoListFiles = true;
                DoListFileSizes = true;

                return 0;
            }

            foreach (var argument in args)
            {
                if (argument.ToLower().Contains("-directories") || argument.ToLower().Contains("d"))
                    DoListDirectories = true;

                if (argument.ToLower().Contains("-size") || argument.ToLower().Contains("s"))
                {
                    DoListFiles = true;
                    DoListFileSizes = true;
                }

                if (argument.ToLower().Contains("-files") || argument.ToLower().Contains("f"))
                    DoListFiles = true;
            }

            return 0;
        }

        static int ListDirectories(string path)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            foreach (var directory in Directory.GetDirectories(path))
            {
                var lastSeparator = (directory.LastIndexOf("/") > 0) ? "/" : "\\";

                Console.WriteLine(directory.Substring(directory.LastIndexOf(lastSeparator) + 1));
            }

            Console.ForegroundColor = ConsoleColor.White;

            return 0;
        }

        static int ListFiles(string path)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            foreach (var file in Directory.GetFiles(path))
            {
                var lastSeparator = (file.LastIndexOf("/") > 0) ? "/" : "\\";

                if (DoListFileSizes == false)
                { 
                    Console.WriteLine(file.Substring(file.LastIndexOf(lastSeparator) + 1));
                }
                else
                {
                    /// TODO I have to read the whole file?
                    byte[] bytes = File.ReadAllBytes(file);
                    Console.WriteLine(file.Substring(file.LastIndexOf(lastSeparator) + 1) + "  " + bytes.Length.ToString() + " bytes");
                }
            }

            Console.ForegroundColor = ConsoleColor.White;

            return 0;
        }
    }
}
