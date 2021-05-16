using System;
using System.Collections.Generic;
using System.IO;

namespace lsd
{
    static class DirectoryManager
    {
        #region GetDirectoryItems
        /// <summary>
        /// Returns list of directory items
        /// </summary>
        /// <param name="path">Folder path</param>
        /// <returns>List of DirectoryItems</returns>
        private static List<DirectoryItem> GetDirectoryItems(string path)
        {
            SortedList<string, DirectoryItem> sorted = new();

            DirectoryInfo dir = new(path);

            foreach (DirectoryInfo directory in dir.GetDirectories())
                sorted.Add(directory.FullName, new DirectoryItem(true, directory.FullName, 0));

            foreach (var file in dir.GetFiles())
                sorted.Add(file.FullName, new DirectoryItem(false, file.FullName, file.Length));

            var items = new List<DirectoryItem>();

            foreach (var item in sorted)
                items.Add(new DirectoryItem(item.Value.IsDirectory, item.Key, item.Value.Length));

            return items;
        }
        #endregion

        #region ShowDirectoryItems
        /// <summary>
        /// Writes out directory items
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <param name="showDirectories">Show directories, or not</param>
        /// <param name="showFiles">Show files, or not</param>
        /// <param name="showAsList">Show as list, or not</param>
        /// <param name="showLength">Show file length, or not</param>
        /// <param name="showBatch">Show batch strings, or not</param>
        public static int ShowDirectoryItems(string path, bool showDirectories, bool showFiles, bool showAsList, bool showLength, bool showBatch)
        {
            var items = GetDirectoryItems(path);

            if (items.Count == 0)
                return 0;

            ConsoleColor previousColor = Console.ForegroundColor;

            foreach (var item in items)
            {
                Console.ForegroundColor = item.Color;

                if (item.IsDirectory && showDirectories)
                {
                    if (showAsList)
                        Console.WriteLine(item.Name);
                    else
                        Console.Write(item.Name + " ");
                }

                if (item.IsDirectory == false && showFiles)
                {
                    if (showAsList)
                    {
                        if (showLength)
                            Console.WriteLine(item.Name + " " + item.Length + " bytes");
                        else
                            Console.WriteLine(item.Name);
                    }
                    else
                    {
                        if (showLength)
                            Console.Write(item.Name + " " + item.Length + " bytes ");
                        else
                            Console.Write(item.Name + " ");
                    }
                }
            }

            if ((showDirectories || showFiles) && showAsList == false)
                Console.WriteLine();

            Console.ForegroundColor = previousColor;

            return 0;
        }
        #endregion
    }
}
