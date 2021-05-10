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

        #region ListDirectoryItems
        /// <summary>
        /// Writes out directory items
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <param name="showDirectories">Show only directories</param>
        /// <param name="showFiles">Show only files</param>
        /// <param name="showAsList">Show as list, or not</param>
        /// <param name="showLength">Show file length, or not</param>
        /// <param name="showBatch">Show batch strings</param>
        public static int ListDirectoryItems(string path, bool showDirectories, bool showFiles, bool showAsList, bool showLength, bool showBatch)
        {
            var items = GetDirectoryItems(path);

            if (items.Count == 0)
                return 0;

            ConsoleColor previousColor = Console.ForegroundColor;

            // TODO Make it simpler, if possible
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
                        if (item.IsDirectory == false && showFiles)
                        {
                            if (showLength)
                                Console.WriteLine(item.Name + " " + item.Length + " bytes");
                            else
                                Console.WriteLine(item.Name);
                        }
                    }
                }
                else
                {
                    if (item.IsDirectory && showDirectories)
                    {
                        Console.Write(item.Name + " ");
                    }
                    else
                    {
                        if (item.IsDirectory == false && showFiles)
                        {
                            if (showLength)
                                Console.Write(item.Name + " " + item.Length + " ");
                            else
                                Console.Write(item.Name + " ");
                        }
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
