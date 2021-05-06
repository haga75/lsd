using System;
using System.Collections.Generic;
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
        static bool DoListItemsAsList = false;

        static int Main(string[] args)
        {
            ParseArgs(args);

            DirectoryPath = Directory.GetCurrentDirectory();

            ListAllDirectoryItems(DirectoryPath);

            return 0;
        }

        static void ParseArgs(string[] args)
        {
            if (args.Length == 0)
            { 
                DoListDirectories = true;
                DoListFiles = true;
                DoListFileSizes = false;
                DoListItemsAsList = false;
            }

            var arg = "";

            foreach (var argument in args)
            {
                arg = argument.ToLower();

                if (arg.Contains("-directories") || arg.Contains("d"))
                    DoListDirectories = true;

                if (arg.Contains("-size") || arg.Contains("s"))
                {
                    DoListFiles = true;
                    DoListFileSizes = true;
                }

                if (arg.Contains("-files") || arg.Contains("f"))
                    DoListFiles = true;

                if (arg.Contains("-list") || arg.Contains("l"))
                    DoListItemsAsList = true;
            }
        }

        static void ListAllDirectoryItems(string path)
        {
            var a = DoListDirectories;
            var b = DoListFiles;
            var c = DoListFileSizes;
            var d = DoListItemsAsList;
            var items = GetAllDirectoryEntriesSorted(path);

            ConsoleColor previousColor = Console.ForegroundColor;

            if (DoListItemsAsList)
            { 
                foreach(var item in items)
                {
                    Console.ForegroundColor = item.Color;
                    Console.WriteLine(item.Name + " " + item.Size + " bytes ");
                }
            }
            else
            {
                foreach (var item in items)
                {
                    Console.ForegroundColor = item.Color;
                    Console.Write(item.Name + " ");
                }
            }

            Console.ForegroundColor = previousColor;
        }

        static List<DirectoryItem> GetAllDirectoryEntriesSorted(string path)
        {
            SortedList<string, DirectoryItem> sorted = new SortedList<string, DirectoryItem>();

            foreach (var directory in Directory.GetDirectories(path))
                sorted.Add(directory, new DirectoryItem(true, directory));

            foreach (var file in Directory.GetFiles(path))
                sorted.Add(file, new DirectoryItem(false, file));

            var items = new List<DirectoryItem>();

            foreach (var item in sorted)
                items.Add(new DirectoryItem(item.Value.IsDirectory, item.Key));


            return items;
        }
    }
}
