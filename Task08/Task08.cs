using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Task08
{
    public static class Task08
    {
        public static int Star1()
        {
            // Coordinates Y, X
            Dictionary<char, List<(int Y, int X)>> coordinates = new();
            int maxX = -1;
            int maxY = 0;
            HashSet<(int, int)> points = new();

            var filename = AocConstants.APP_FOLDER + "Task08\\Task081.txt";
            const int BufferSize = 512;

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.Length > maxX) maxX = line.Length;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] != '.')
                        {
                            if (coordinates.ContainsKey(line[i]))
                            {
                                coordinates[line[i]].Add((Y: maxY, X: i));
                            }
                            else
                            {
                                coordinates.Add(line[i], new List<(int Y, int X)> { (maxY, i) });
                            }
                        }
                    }

                    maxY++;
                }


                foreach (var item in coordinates)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        for (int j = i + 1; j < item.Value.Count; j++)
                        {
                            // Find antinodes for this line
                            var deltaX = item.Value[j].X - item.Value[i].X;
                            var deltaY = item.Value[j].Y - item.Value[i].Y;

                            (int Y, int X) newPoint1 = (item.Value[j].Y + deltaY, item.Value[j].X + deltaX);
                            (int Y, int X) newPoint2 = (item.Value[i].Y - deltaY, item.Value[i].X - deltaX);

                            if (IsPointInGrid(newPoint1, maxX, maxY)) points.Add(newPoint1);
                            if (IsPointInGrid(newPoint2, maxX, maxY)) points.Add(newPoint2);
                        }
                    }
                }

            }

            return points.Count;
        }


        private static bool IsPointInGrid((int Y, int X) point, int maxX, int maxY)
        {
            return !(point.Y < 0 || point.X < 0 || point.Y >= maxY || point.X >= maxX);
        }


        public static int Star2()
        {
            // Coordinates Y, X
            Dictionary<char, List<(int Y, int X)>> coordinates = new();
            int maxX = -1;
            int maxY = 0;
            HashSet<(int, int)> points = new();

            var filename = AocConstants.APP_FOLDER + "Task08\\Task081.txt";
            const int BufferSize = 512;

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.Length > maxX) maxX = line.Length;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] != '.')
                        {
                            if (coordinates.ContainsKey(line[i]))
                            {
                                coordinates[line[i]].Add((Y: maxY, X: i));
                            }
                            else
                            {
                                coordinates.Add(line[i], new List<(int Y, int X)> { (maxY, i) });
                            }
                        }
                    }

                    maxY++;
                }


                // Algorithm starts here
                foreach (var item in coordinates)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        for (int j = i + 1; j < item.Value.Count; j++)
                        {
                            
                            points.Add(item.Value[i]);

                            // Find antinodes for this line
                            var deltaX = item.Value[j].X - item.Value[i].X;
                            var deltaY = item.Value[j].Y - item.Value[i].Y;

                            bool isFirst = true;
                            bool isSecond = true;
                            (int Y, int X) newPoint1 = item.Value[i];   // They must be the same!
                            (int Y, int X) newPoint2 = item.Value[i];
                            while (true)
                            {
                                if (isFirst && IsPointInGrid(newPoint1, maxX, maxY)) points.Add(newPoint1); else isFirst = false;
                                if (isSecond && IsPointInGrid(newPoint2, maxX, maxY)) points.Add(newPoint2); else isSecond = false;

                                newPoint1 = (newPoint1.Y + deltaY, newPoint1.X + deltaX);
                                newPoint2 = (newPoint2.Y - deltaY, newPoint2.X - deltaX);

                                if (!isFirst && !isSecond) break;
                            }
                        }
                    }
                }

            }

            return points.Count;
        }

    }
}
