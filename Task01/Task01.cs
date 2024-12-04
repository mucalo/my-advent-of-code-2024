using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Task01
{
    public static class Task01
    {
        public static int Star1()
        {
            int sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task01\\Task011.txt";
            const int BufferSize = 128;

            List<int> firstList = new List<int>();
            List<int> secondList = new List<int>();

            using (var fileStream = File.OpenRead(filename))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var items = line.Split("   ");
                    int firstNum = Convert.ToInt32(items[0]);
                    int secondNum = Convert.ToInt32(items[1]);

                    firstList.Add(firstNum);
                    secondList.Add(secondNum);
                    }
                }

            firstList.Sort();
            secondList.Sort();

            for (int i=0; i<firstList.Count; i++)
            {
                sum += Math.Abs(firstList[i] - secondList[i]);
            }

            return sum;
        }

        public static int Star2()
        {
            int sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task01\\Task011.txt";
            const int BufferSize = 128;

            List<int> firstList = new List<int>();
            List<int> secondList = new List<int>();

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var items = line.Split("   ");
                    int firstNum = Convert.ToInt32(items[0]);
                    int secondNum = Convert.ToInt32(items[1]);

                    firstList.Add(firstNum);
                    secondList.Add(secondNum);
                }
            }

            for (int i = 0; i < firstList.Count; i++)
            {
                var count = secondList.Count(item => item == firstList[i]);
                sum += firstList[i] * count;
            }

            return sum;
        }


    }
}
