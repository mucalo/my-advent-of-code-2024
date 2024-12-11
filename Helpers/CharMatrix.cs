using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Helpers
{
    public class CharMatrix
    {
        public Dictionary<(int Y, int X), char> Matrix { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < MaxY; i++)
            {
                for (int j = 0; j < MaxX; j++)
                {
                    if (Matrix.ContainsKey((Y: i, X: j)))
                    {
                        sb.Append(Matrix[(Y: i, X: j)]);
                    }
                    else
                    {
                        sb.Append('.');
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
