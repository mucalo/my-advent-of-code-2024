using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Task04
{
    public static class Task04
    {
        public static int Star1()
        {
            int count = 0;

            int sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task04\\Task041.txt";
            const int BufferSize = 512;

            char[][] map = new char[140][];

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                int i = 0;
                int maxX = 0;
                int maxY = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    map[i] = line.ToCharArray();
                    if (map[i].Length > maxX) maxX = map[i].Length;

                    i++;
                }

                maxY = i;

                for (i = 0; i < maxY; i++)
                {
                    for (int j = 0; j < map[i].Length; j++)
                    {
                        count += HowManyXmas(j, i, map, maxX, maxY);
                    }
                }

            }
            return count;
        }


        public static int Star2()
        {
            int count = 0;

            int sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task04\\Task041.txt";
            const int BufferSize = 512;

            char[][] map = new char[140][];

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                int i = 0;
                int maxX = 0;
                int maxY = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    map[i] = line.ToCharArray();
                    if (map[i].Length > maxX) maxX = map[i].Length;

                    i++;
                }

                maxY = i;

                for (i = 1; i < maxY - 1; i++)
                {
                    for (int j = 1; j < map[i].Length - 1; j++)
                    {
                        // count += HowManyXmas(j, i, map, maxX, maxY);
                        count += HowManyX_MAS(j, i, map, maxX, maxY);
                    }
                }

            }
            return count;
        }

        private static int HowManyX_MAS(int x, int y, char[][] map, int maxX, int maxY)
        {
            bool isA = map[y][x] == 'A';
            bool diag1 = (map[y - 1][x - 1] == 'M' && map[y + 1][x + 1] == 'S') || (map[y - 1][x - 1] == 'S' && map[y + 1][x + 1] == 'M');
            bool diag2 = (map[y - 1][x + 1] == 'M' && map[y + 1][x - 1] == 'S') || (map[y - 1][x + 1] == 'S' && map[y + 1][x - 1] == 'M');

            if (isA && diag1 && diag2) return 1; else return 0;
        }

        private static int HowManyXmas(int x, int y, char[][] map, int maxX, int maxY)
        {
            int count = 0;

            if (map[y][x] != 'X') return 0;

            // up
            if (y > 2)
            {
                if (map[y - 1][x] == 'M' && map[y - 2][x] == 'A' && map[y - 3][x] == 'S') count++;
            }

            // down
            if (y < maxY - 3)
            {
                if (map[y + 1][x] == 'M' && map[y + 2][x] == 'A' && map[y + 3][x] == 'S') count++;
            }

            // left
            if (x > 2)
            {
                if (map[y][x - 1] == 'M' && map[y][x - 2] == 'A' && map[y][x - 3] == 'S') count++;
            }

            // right
            if (x < maxX - 3)
            {
                if (map[y][x + 1] == 'M' && map[y][x + 2] == 'A' && map[y][x + 3] == 'S') count++;
            }

            // up-left
            if (y > 2 && x > 2)
            {
                if (map[y - 1][x - 1] == 'M' && map[y - 2][x - 2] == 'A' && map[y - 3][x - 3] == 'S') count++;
            }

            // up-right
            if (y > 2 && x < maxX - 3)
            {
                if (map[y - 1][x + 1] == 'M' && map[y - 2][x + 2] == 'A' && map[y - 3][x + 3] == 'S') count++;
            }
            // down-left
            if (y < maxY - 3 && x > 2)
            {
                if (map[y + 1][x - 1] == 'M' && map[y + 2][x - 2] == 'A' && map[y + 3][x - 3] == 'S') count++;
            }

            // down-right
            if (y < maxY - 3 && x < maxX - 3)
            {
                if (map[y + 1][x + 1] == 'M' && map[y + 2][x + 2] == 'A' && map[y + 3][x + 3] == 'S') count++;
            }


            return count;
        }
    }
}
