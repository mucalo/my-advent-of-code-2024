using AdventOfCode2024.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Task06
{
    public static class Task06
    {
        public static char WALL = '#';
        public static char MARK = 'X';

        public static int Star1()
        {
            var filename = AocConstants.APP_FOLDER + "Task06\\Task061.txt";
            const int BufferSize = 512;
            CharMatrix matrix = new CharMatrix();
            matrix.Matrix = new();
            int direction = 0;  // 0 = up, 1 = right, 2 = down, 3 = left;



            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {

                // Reading data
                string line;
                int row = 0;
                int initialY = 0;
                int initialX = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        // Set initial values
                        if (line[i] == '^')
                        {
                            initialX = i;
                            initialY = row;
                        }

                        // Add Xes
                        if (line[i] != '.')
                        {
                            matrix.Matrix.Add((Y: row, X: i), line[i]);
                        }


                    }
                    if (row == 0) matrix.MaxX = line.Length;

                    row++;
                }
                matrix.MaxY = row;
                matrix.Matrix[(Y: initialY, X: initialX)] = 'X';

                // Console.WriteLine(matrix);


                // WORK
                bool isOut = false;
                int currentY = initialY;
                int currentX = initialX;
                while (!isOut)
                {
                    var result = ToNearestNeighbour(matrix, currentY, currentX, direction);

                    direction = (++direction) % 4;
                    isOut = result.IsOut;
                    currentX = result.X;
                    currentY = result.Y;

                    //Console.WriteLine(matrix);
                    //Console.ReadKey();
                }

            }

            return matrix.Matrix.Values.Count(c => c == MARK);
        }

        public static int Star2()
        {
            var filename = AocConstants.APP_FOLDER + "Task06\\Example.txt";
            const int BufferSize = 512;
            CharMatrix matrix = new CharMatrix();
            matrix.Matrix = new();
            int direction = 0;  // 0 = up, 1 = right, 2 = down, 3 = left;
            int sum = 0;


            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {

                // Reading data
                string line;
                int row = 0;
                int initialY = 0;
                int initialX = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        // Set initial values
                        if (line[i] == '^')
                        {
                            initialX = i;
                            initialY = row;
                        }

                        // Add Xes
                        if (line[i] != '.')
                        {
                            matrix.Matrix.Add((Y: row, X: i), line[i]);
                        }


                    }
                    if (row == 0) matrix.MaxX = line.Length;

                    row++;
                }
                matrix.MaxY = row;
                matrix.Matrix[(Y: initialY, X: initialX)] = 'X';

                // Console.WriteLine(matrix);


                // WORK
                bool isOut = false;
                int currentY = initialY;
                int currentX = initialX;

                while (!isOut)
                {
                    var result = ToNearestNeighbourOrLoop(matrix, currentY, currentX, direction);

                    direction = (++direction) % 4;
                    isOut = result.IsOut;
                    currentX = result.X;
                    currentY = result.Y;
                    sum += result.loopCount >= 0 ? result.loopCount : 0;
                    Console.WriteLine("So far: " + sum);

                    //Console.WriteLine(matrix);
                    //Console.ReadKey();
                }

            }

            return sum;
        }



        private static (int Y, int X, bool IsOut) ToNearestNeighbour(CharMatrix matrix, int y, int x, int direction)
        {
            bool isOut = false;


            // UP
            if (direction == 0)
            {
                bool isBreak = false;
                do
                {
                    matrix.Matrix[(y, x)] = 'X';

                    if (y == 0)
                    {
                        // If we got to the top of the board, we're out!
                        isBreak = true;
                        isOut = true;
                        break;
                    }
                    else if (matrix.Matrix.ContainsKey((y - 1, x)) && matrix.Matrix[(y - 1, x)] == WALL)
                    {
                        // We hit a wall, return
                        isBreak = true;
                        break;
                    }
                    y--;

                } while (!isBreak);
            }

            // RIGHT
            if (direction == 1)
            {
                bool isBreak = false;
                do
                {
                    matrix.Matrix[(y, x)] = 'X';

                    if (x == matrix.MaxX - 1)
                    {
                        // If we got to the top of the board, we're out!
                        isBreak = true;
                        isOut = true;
                        break;
                    }
                    else if (matrix.Matrix.ContainsKey((y, x + 1)) && matrix.Matrix[(y, x + 1)] == WALL)
                    {
                        // We hit a wall, return
                        isBreak = true;
                        break;
                    }
                    x++;

                } while (!isBreak);
            }

            // DOWN
            if (direction == 2)
            {
                bool isBreak = false;
                do
                {
                    matrix.Matrix[(y, x)] = 'X';

                    if (y == matrix.MaxY - 1)
                    {
                        // If we got to the top of the board, we're out!
                        isBreak = true;
                        isOut = true;
                        break;
                    }
                    else if (matrix.Matrix.ContainsKey((y + 1, x)) && matrix.Matrix[(y + 1, x)] == WALL)
                    {
                        // We hit a wall, return
                        isBreak = true;
                        break;
                    }
                    y++;

                } while (!isBreak);
            }

            // LEFT
            if (direction == 3)
            {
                bool isBreak = false;
                do
                {
                    matrix.Matrix[(y, x)] = 'X';

                    if (x == 0)
                    {
                        // If we got to the top of the board, we're out!
                        isBreak = true;
                        isOut = true;
                        break;
                    }
                    else if (matrix.Matrix.ContainsKey((y, x - 1)) && matrix.Matrix[(y, x - 1)] == WALL)
                    {
                        // We hit a wall, return
                        isBreak = true;
                        break;
                    }
                    x--;

                } while (!isBreak);
            }


            return (y, x, isOut);
        }


        /// <summary>
        /// MEthod returns bool depending whether a break after this intersection in given direction is a loop.
        /// </summary>
        /// <param name="inMatrixChar"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private static bool IsLoop(char inMatrixChar, int direction)
        {
            bool isLoop = false;

            byte myByte = (byte)inMatrixChar;
            byte nextDirectionByte = ByteOperations.SetBit(0, (direction + 1) % 4);
            var exists = ByteOperations.And4Bit(myByte, nextDirectionByte);     
            if (exists != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns (direction+1).ToBinary()
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private static char GetCharPerDirection(int direction)
        {
            if (direction == 0) return (char)0b0001;    // return byte 1
            if (direction == 1) return (char)0b0010;    // return byte 2
            if (direction == 2) return (char)0b0011;    // return byte 3
            if (direction == 3) return (char)0b0100;    // return byte 4
            else return (char)0b0;
        }

        private static char UpdateCharWithDirection(char inMatrixChar, int direction)
        {
            byte myByte = (byte)inMatrixChar;
            byte directionByte = (byte)GetCharPerDirection(direction);
            byte toReturn = (byte)(myByte | directionByte);
            return (char)toReturn;
        }
        



        private static (int Y, int X, bool IsOut, int loopCount) ToNearestNeighbourOrLoop(CharMatrix matrix, int y, int x, int direction)
        {
            bool isOut = false;
            int loopCount = 0;

            // UP
            if (direction == 0)
            {
                bool isBreak = false;
                do
                {
                    if (matrix.Matrix.ContainsKey((y, x)))
                    {
                        if (IsLoop(matrix.Matrix[(y,x)], direction))
                        {
                            loopCount++;
                        }
                        matrix.Matrix[(y,x)] = UpdateCharWithDirection(matrix.Matrix[(y,x)], direction);
                        Console.WriteLine(matrix);
                        Console.ReadKey();
                    }
                    else matrix.Matrix[(y, x)] = GetCharPerDirection(direction);

                    if (y == 0)
                    {
                        // If we got to the top of the board, we're out!
                        isBreak = true;
                        isOut = true;
                        break;
                    }
                    else if (matrix.Matrix.ContainsKey((y - 1, x)) && matrix.Matrix[(y - 1, x)] == WALL)
                    {
                        // We hit a wall, return
                        isBreak = true;
                        break;
                    }
                    y--;

                } while (!isBreak);
            }

            // RIGHT
            if (direction == 1)
            {
                bool isBreak = false;
                do
                {
                    if (matrix.Matrix.ContainsKey((y, x)))
                    {
                        if (IsLoop(matrix.Matrix[(y, x)], direction))
                        {
                            loopCount++;
                        }
                        matrix.Matrix[(y, x)] = UpdateCharWithDirection(matrix.Matrix[(y, x)], direction);
                        Console.WriteLine(matrix);
                        Console.ReadKey();
                    }
                    else matrix.Matrix[(y, x)] = GetCharPerDirection(direction);

                    if (x == matrix.MaxX - 1)
                    {
                        // If we got to the top of the board, we're out!
                        isBreak = true;
                        isOut = true;
                        break;
                    }
                    else if (matrix.Matrix.ContainsKey((y, x + 1)) && matrix.Matrix[(y, x + 1)] == WALL)
                    {
                        // We hit a wall, return
                        isBreak = true;
                        break;
                    }
                    x++;

                } while (!isBreak);
            }

            // DOWN
            if (direction == 2)
            {
                bool isBreak = false;
                do
                {
                    if (matrix.Matrix.ContainsKey((y, x)))
                    {
                        if (IsLoop(matrix.Matrix[(y, x)], direction))
                        {
                            loopCount++;
                        }
                        matrix.Matrix[(y, x)] = UpdateCharWithDirection(matrix.Matrix[(y, x)], direction);
                        Console.WriteLine(matrix);
                        Console.ReadKey();
                    }
                    else matrix.Matrix[(y, x)] = GetCharPerDirection(direction);

                    if (y == matrix.MaxY - 1)
                    {
                        // If we got to the top of the board, we're out!
                        isBreak = true;
                        isOut = true;
                        break;
                    }
                    else if (matrix.Matrix.ContainsKey((y + 1, x)) && matrix.Matrix[(y + 1, x)] == WALL)
                    {
                        // We hit a wall, return
                        isBreak = true;
                        break;
                    }
                    y++;

                } while (!isBreak);
            }

            // LEFT
            if (direction == 3)
            {
                bool isBreak = false;
                do
                {
                    if (matrix.Matrix.ContainsKey((y, x)))
                    {
                        if (IsLoop(matrix.Matrix[(y, x)], direction))
                        {
                            loopCount++;
                        }
                        matrix.Matrix[(y, x)] = UpdateCharWithDirection(matrix.Matrix[(y, x)], direction);
                        Console.WriteLine(matrix);
                        Console.ReadKey();
                    }
                    else matrix.Matrix[(y, x)] = GetCharPerDirection(direction);

                    if (x == 0)
                    {
                        // If we got to the top of the board, we're out!
                        isBreak = true;
                        isOut = true;
                        break;
                    }
                    else if (matrix.Matrix.ContainsKey((y, x - 1)) && matrix.Matrix[(y, x - 1)] == WALL)
                    {
                        // We hit a wall, return
                        isBreak = true;
                        break;
                    }
                    x--;

                } while (!isBreak);
            }


            return (y, x, isOut, loopCount - 1);
        }
    }
}
