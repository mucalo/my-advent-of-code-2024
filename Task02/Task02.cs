using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Task02
{
    public static class Task02
    {
        public static int Star1()
        {
            int sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task02\\Task021.txt";
            const int BufferSize = 128;


            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var items = line.Split(" ");
                    bool isSafe = IsSafe(items);
                    if (isSafe) sum++;
                }
            }
            return sum;
        }

        public static int Star2()
        {
            int sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task02\\Task021.txt";
            const int BufferSize = 128;


            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var allItems = line.Split(" ").ToList();
                    
                    bool isSafe = IsSafe(allItems.ToArray());
                    if (isSafe) { sum++; continue; }

                    for (int i = 0; i < allItems.Count; i++)
                    {
                        // Prepare the items by removing the item at index i:
                        List<string> tempItems = new List<string>();
                        tempItems.AddRange(allItems);
                        tempItems.RemoveAt(i);
                        var items = tempItems.ToArray();

                        isSafe = IsSafe(items);
                        if (isSafe) { sum++; break; }
                    }
                }
            }
            return sum;
        }



        private static bool IsSafe(string[] items)
        {
            bool isIncreasing = Convert.ToInt32(items[0]) < Convert.ToInt32(items[1]);
            bool isSafe = true;

            int currentItem = Convert.ToInt32(items[0]);

            for (int i = 1; i < items.Length; i++)
            {
                int newItem = Convert.ToInt32(items[i]);

                if (newItem < currentItem && isIncreasing) { isSafe = false; break; }
                if (newItem > currentItem && !isIncreasing) { isSafe = false; break; }
                int delta = Math.Abs(newItem - currentItem);
                if (delta < 1 || delta > 3) { isSafe = false; break; }

                currentItem = newItem;
            }

            return isSafe;
        }
    }
}
