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
                    int spaces = (int)char.GetNumericValue(chars[beginning + 1]);
                    for (int i = 0; i < n; i++)
                    {
                        output.Add(beginningNumber);
                        sum += (long)beginningNumber * (output.Count - 1);
                    }


                    // Check if there is anything else left in the end, if not, break
                    if (beginning >= end)
                        break;


                    // Add end
                    while (true)
                    {
                        var nLast = (int)char.GetNumericValue(chars[end]);
                        if (nLast <= spaces)
                        {
                            for (int i = 0; i < nLast; i++)
                            {
                                output.Add(endNumber);
                                sum += (long)endNumber * (output.Count - 1);
                                spaces--;
                            }

                            end = end - 2;
                            endNumber--;

                            if (nLast == spaces) break; // break the inner while loop
                        }
                        else
                        {
                            for (int i = 0; i < spaces; i++)
                            {
                                output.Add(endNumber);
                                sum += (long)endNumber * (output.Count - 1);
                            }
                            chars[end] = (char)(((int)char.GetNumericValue(chars[end]) - spaces) + '0');
                            break;
                        }
                    }



                    beginning += 2;
                    beginningNumber++;
                }
                Console.WriteLine(string.Join(",", output));
            }




            return sum;
        }
    }
}
