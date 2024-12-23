using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Task10
{
    public static class Task10
    {
        public const int TOP = 9;
        public const int START = 0;
        public static int Star1()
        {
            var filename = AocConstants.APP_FOLDER + "Task10\\Task101.txt";
            const int BufferSize = 512;
            int maxX = 0, maxY = 0;
            Dictionary<(int Y, int X), int> matrix = new Dictionary<(int, int), int>();
            List<(int Y, int X)> startingPoints = new();
            List<(int Y, int X)> visitedTops = new();

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.Length > maxX) maxX = line.Length;

                    for (int i = 0; i < line.Length; i++)
                    {
                        matrix.Add((maxY, i), Convert.ToInt32(line[i].ToString()));
                        if (matrix[(maxY, i)] == START) { startingPoints.Add((maxY, i)); }
                    }
                    maxY++;
                }
            }

            return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="point">Point we're currently looking at</param>
        /// <param name="lastDirection">Direction we used to get here</param>
        /// <param name="currentPath">All points in the current path</param>
        /// <param name="visitedTops">All distinct tops visited so far</param>
        /// <param name="matrix"></param>
        /// <param name="maxY"></param>
        /// <param name="maxX"></param>
        /// <returns></returns>
        public static int RecFunction((int Y, int X) point, int lastDirection, List<(int, int)> currentPath, ref List<(int, int)> visitedTops, Dictionary<(int, int), int> matrix, int maxY, int maxX)
        {
            // If all four directions are smaller, return -1
            // If we hit a TOP and it's not already visited, return 1 and add it to the visited list
            // Try 3 other directions
            return 0;
        }
    }
}
