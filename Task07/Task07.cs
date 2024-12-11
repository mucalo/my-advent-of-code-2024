using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Task07
{
    public static class Task07
    {
        public static long Star1()
        {
            long sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task07\\Task071.txt";
            const int BufferSize = 512;

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var items = line.Split(": ");
                    long target = Convert.ToInt64(items[0]);
                    var numbers = items[1].Split(' ');

                    var partial = new HashSet<long>
                    {
                        Convert.ToInt64(numbers[0])
                    };

                    for (int i = 1; i < numbers.Length; i++)
                    {
                        long number = Convert.ToInt64(numbers[i]);
                        List<long> partials = [.. partial];
                        partial = new HashSet<long>();

                        foreach(var partialResult in partials)
                        {
                            partial.Add(partialResult + number);
                            partial.Add(partialResult * number);
                        }
                    }

                    // Check if tatget is in hash
                    if (partial.Contains(target))
                    {
                        sum += target;
                    }
                }
            }
            return sum;
        }

        public static long Star2()
        {
            long sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task07\\Task071.txt";
            const int BufferSize = 512;

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var items = line.Split(": ");
                    long target = Convert.ToInt64(items[0]);
                    var numbers = items[1].Split(' ');

                    var partial = new HashSet<long>
                    {
                        Convert.ToInt64(numbers[0])
                    };

                    for (int i = 1; i < numbers.Length; i++)
                    {
                        long number = Convert.ToInt64(numbers[i]);
                        List<long> partials = [.. partial];
                        partial = new HashSet<long>();

                        foreach (var partialResult in partials)
                        {
                            partial.Add(partialResult + number);
                            partial.Add(partialResult * number);
                            partial.Add(Convert.ToInt64(partialResult.ToString() + number.ToString()));
                        }
                    }

                    // Check if tatget is in hash
                    if (partial.Contains(target))
                    {
                        sum += target;
                    }
                }
            }
            return sum;
        }
    }
}
