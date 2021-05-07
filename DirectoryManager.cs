using System;
using System.Collections.Generic;
using System.IO;

namespace lsd
{
    static class DirectoryManager
    {
        private static List<DirectoryItem> GetAllDirectoryItemsSorted(string path)
        {
            SortedList<string, DirectoryItem> sorted = new();

            DirectoryInfo ThisDirectory = new(path);

            foreach (DirectoryInfo directory in ThisDirectory.GetDirectories())
                sorted.Add(directory.FullName, new DirectoryItem(true, directory.FullName, 0));

            foreach (var file in ThisDirectory.GetFiles())
                sorted.Add(file.FullName, new DirectoryItem(false, file.FullName, file.Length));

            var items = new List<DirectoryItem>();

            foreach (var item in sorted)
                items.Add(new DirectoryItem(item.Value.IsDirectory, item.Key, item.Value.Length));

            return items;
        }

        public static void ListAllDirectoryItems(string path, bool showDirectories, bool showFiles, bool showList)
        {
            var items = DirectoryManager.GetAllDirectoryItemsSorted(path);

            ConsoleColor previousColor = Console.ForegroundColor;

            if (showList)
            {
                foreach (var item in items)
                {
                    Console.ForegroundColor = item.Color;

                    if (item.IsDirectory)
                        Console.WriteLine(item.Name);
                    else
                        Console.WriteLine(item.Name + " " + item.Length + " bytes ");
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
    }
}
