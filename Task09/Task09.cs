using AdventOfCode2024.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Task09
{
    public static class Task09
    {
        public static long Star1()
        {
            long sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task09\\Task091.txt";
            const int BufferSize = 512;


            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {

                // Reading data
                string line;
                line = streamReader.ReadLine();
                char[] chars = line.ToCharArray();

                List<int> output = new List<int>();

                // COntrol mechanism
                long ctrlSum = 0;
                for (int i = 0; i < chars.Length; i++)
                {
                    var currNum = (int)char.GetNumericValue(chars[i]);
                    if (i % 2 == 0) ctrlSum += currNum;
                }

                int beginning = 0;
                int end = line.Length - 1;
                int beginningNumber = 0;
                int endNumber = line.Length / 2;

                // Key is the value, value is the index of first occurence of that value in the new order
                Dictionary<int, int> indexOfValues = new Dictionary<int, int>();

                while (true)
                {
                    // Add beginning
                    int n = (int)char.GetNumericValue(chars[beginning]);

                    for (int i = 0; i < n; i++)
                    {
                        output.Add(beginningNumber);
                        sum += (long)beginningNumber * (output.Count - 1);

                        if (output.Count == ctrlSum)
                            goto thisistheend;
                    }


                    // Check if there is anything else left in the end, if not, break
                    string meho = "";
                    if (beginning >= end)
                        meho += "hjksh";


                    int spaces = (int)char.GetNumericValue(chars[beginning + 1]);


                    // Add end
                    while (true)
                    {
                        var nLast = (int)char.GetNumericValue(chars[end]);
                        if (nLast <= spaces)
                        {
                            for (int i = 0; i < nLast; i++)
                            {
                                output.Add(-1 * endNumber);
                                sum += (long)endNumber * (output.Count - 1);

                                if (output.Count == ctrlSum)
                                    goto thisistheend;
                                spaces--;
                            }

                            end = end - 2;
                            endNumber--;

                            // if (nLast == spaces) break; // break the inner while loop
                        }
                        else
                        {
                            for (int i = 0; i < spaces; i++)
                            {
                                output.Add(-1 * endNumber);
                                sum += (long)endNumber * (output.Count - 1);

                                if (output.Count == ctrlSum)
                                    goto thisistheend;

                            }
                            chars[end] = (char)(((int)char.GetNumericValue(chars[end]) - spaces) + '0');
                            break;
                        }
                    }



                    beginning += 2;
                    beginningNumber++;


                }

thisistheend:
                Console.WriteLine(string.Join(",", output));
            }




            return sum;
        }

        public static long Star2()
        {
            long sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task09\\Example.txt";
            const int BufferSize = 512;


            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {

                // Reading data
                string line = streamReader.ReadLine();
                int[] digits = line.Select(c => int.Parse(c.ToString())).ToArray();
                int beginningNumber = 0;
                int beginning = 0;
                int end = digits.Length - 1;
                int endNumber = digits.Length / 2;
                List<int> output = new();


                // Custom comparer for descending order
                var comparer = Comparer<int>.Create((x, y) => y.CompareTo(x));
                // Dictionary will have number of spaces as a key and list of indexes as a value
                SortedDictionary<int, SortedSet<int>> spaces = new(comparer);
                int index = 0;
                for (int i = 0; i < digits.Length; i++)
                {

                    // if the number is odd, then create spaces in the dictionary
                    if (i % 2 != 0)
                    {
                        if (!spaces.ContainsKey(digits[i]))
                            spaces[digits[i]] = new SortedSet<int>();
                        spaces[digits[i]].Add(index);
                    }
                    // increase the index by the amount of chars
                    index += digits[i];

                }


                
            }


            return sum;
        }
    }
}
