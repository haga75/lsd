using System;
using System.Collections.Generic;
using System.IO;

namespace lsd
{
    static class DirectoryManager
    {
        #region GetAllDirectoryItemsSorted
        /// <summary>
        /// Returns list of directory items
        /// </summary>
        /// <param name="path">Current folder path</param>
        /// <returns>List of DirectoryItems</returns>
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
        #endregion

        #region ListAllDirectoryItems
        /// <summary>
        /// Writes out directory items
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <param name="showDirectories">Show only directories</param>
        /// <param name="showFiles">Show only files</param>
        /// <param name="showAsList">Show as list, or not</param>
        public static int ListAllDirectoryItems(string path, bool showDirectories = false, bool showFiles = false, bool showAsList = false)
        {
            var items = DirectoryManager.GetAllDirectoryItemsSorted(path);

            if (items.Count == 0)
                return 0;

            ConsoleColor previousColor = Console.ForegroundColor;

            foreach (var item in items)
            {
                Console.ForegroundColor = item.Color;

                if (showAsList)
                {
                    if (item.IsDirectory && showDirectories)
                    { 
                        Console.WriteLine(item.Name);
                    }
                    else
                    { 
                        if (item.IsDirectory == false)
                        {
                            Console.WriteLine(item.Name + " " + item.Length);
                        }
                    }
                }
                else
                {
                    if (item.IsDirectory && showDirectories)
                    {
                        Console.Write(item.Name +  " ");
                    }
                    else
                    {
                        if (item.IsDirectory == false)
                        {
                            Console.Write(item.Name + " ");
                        }
                    }
                }
            }

            Console.ForegroundColor = previousColor;

            return 0;
        }
        #endregion
    }
}
