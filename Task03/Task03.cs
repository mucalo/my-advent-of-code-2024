using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Task03
{
    public static class Task03
    {
        public static int Star1()
        {
            int sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task03\\Task031.txt";
            const int BufferSize = 512;

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                string pattern = @"mul\(\d+,\d+\)";
                while ((line = streamReader.ReadLine()) != null)
                {
                    sum = MultiplyInString(sum, line, pattern);
                }
            }

            return sum;
        }

        private static int MultiplyInString(int sum, string line, string pattern)
        {
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(line);

            foreach (Match match in matches)
            {
                sum += MultiplyMul(match.ToString());
            }

            return sum;
        }

        private static int MultiplyMul(string mul)
        {
            var substring = mul.Substring(4);
            substring = substring.Substring(0, substring.Length - 1);
            var numbers = substring.Split(',');
            return Convert.ToInt32(numbers[0]) * Convert.ToInt32(numbers[1]);
        }


        public static int Star2()
        {
            int sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task03\\Task031.txt";
            const int BufferSize = 512;
            bool lineStartsWithDo = true;

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                string pattern = @"mul\(\d+,\d+\)";
                while ((line = streamReader.ReadLine()) != null)
                {
                    var currentLine = line;

                    var doSections = line.Split("do()");
                    for (int i = 0; i < doSections.Length; i++)
                    {
                        if (i == 0 && !lineStartsWithDo) continue;

                        var finalSections = doSections[i].Split("don't()");
                        sum = MultiplyInString(sum, finalSections[0], pattern);
                    }

                    // Calculate if next line starts with do or dont
                    var indexOfDo = line.LastIndexOf("do()");
                    var indexOfDont = line.LastIndexOf("don't()");
                    lineStartsWithDo = indexOfDo > indexOfDont;
                }
            }

            return sum;
        }
    }
}
