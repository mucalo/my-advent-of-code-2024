using System.Text;

namespace AdventOfCode2024.Task05
{
    public static class Task05
    {
        public static int Star1()
        {
            int sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task05\\Task051.txt";
            const int BufferSize = 512;

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;

                // key is a number. value is a list of all numbers that MUST NOT be after it!
                Dictionary<int, List<int>> rules = new();
                bool isRule = true;

                while ((line = streamReader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        isRule = false;
                        continue;
                    }

                    if (isRule)
                    {
                        // Process the rules
                        var numbers = line.Split('|');
                        var value = Convert.ToInt32(numbers[0]);
                        var key = Convert.ToInt32(numbers[1]);
                        if (rules.ContainsKey(key))
                        {
                            rules[key].Add(value);
                        }
                        else
                        {
                            rules.Add(key, new List<int>() { value });
                        }
                    }
                    else
                    {
                        var numbers = line.Split(',');
                        int middle = numbers.Length / 2;    // index of middle element
                        int middleValue = 0;
                        List<int> dissallowed = new();
                        for (int i = 0; i < numbers.Length; i++)
                        {
                            var currentNumber = Convert.ToInt32(numbers[i]);

                            if (dissallowed.Contains(currentNumber))
                            {
                                // not allowed due to rule
                                middleValue = 0;
                                break;
                            }
                            else
                            {
                                if (rules.ContainsKey(currentNumber))
                                {
                                    dissallowed.AddRange(rules[currentNumber]);
                                }
                                if (i == middle)
                                {
                                    middleValue = currentNumber;
                                }
                            }
                        }

                        sum += middleValue;
                    }
                }
            }

            return sum;
        }

        public static int Star2()
        {
            int sum = 0;
            var filename = AocConstants.APP_FOLDER + "Task05\\Task051.txt";
            const int BufferSize = 512;

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;

                // key is a number. value is a list of all numbers that MUST NOT be after it!
                Dictionary<int, List<int>> rules = new();
                bool isRule = true;

                while ((line = streamReader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        isRule = false;
                        continue;
                    }

                    if (isRule)
                    {
                        // Process the rules
                        var numbers = line.Split('|');
                        var value = Convert.ToInt32(numbers[0]);
                        var key = Convert.ToInt32(numbers[1]);
                        if (rules.ContainsKey(key))
                        {
                            rules[key].Add(value);
                        }
                        else
                        {
                            rules.Add(key, new List<int>() { value });
                        }
                    }
                    else
                    {
                        var numbers = line.Split(',');
                        int middle = numbers.Length / 2;    // index of middle element
                        int middleValue = 0;
                        Dictionary<int, List<int>> dissallowed = new();
                        bool isCorrupt = false;
                        List<int> disallowedKeys = new();
                        for (int i = 0; i < numbers.Length; i++)
                        {
                            var currentNumber = Convert.ToInt32(numbers[i]);
                                
                            foreach (var key in disallowedKeys)
                            {
                                // if the value from the rules dictionary contains the current number,
                                // then the current number needs to go before the key
                                if (rules.ContainsKey(key) && rules[key].Contains(currentNumber))
                                {
                                    // Fix the corrupt row
                                    var x = Array.IndexOf(numbers, key.ToString());
                                    var temp = numbers[x];
                                    for (int j = 0; j < i - x; j++)
                                    {
                                        var toRemove = Convert.ToInt32(numbers[x + j]);
                                        disallowedKeys.Remove(toRemove);

                                        numbers[x + j] = numbers[x + j + 1];
                                    }

                                    numbers[i] = temp;
                                    isCorrupt = true;
                                    
                                    // Go back to starting from the old key
                                    // as we might have broken something in the mean time
                                    // we also need to remove disallowed keys after the new "current" key
                                    i = x;
                                    var newCurrent = Convert.ToInt32(numbers[i]);
                                    var indexOfDisallowed = disallowedKeys.IndexOf(newCurrent);
                                    break;
                                }
                            }
                            currentNumber = Convert.ToInt32(numbers[i]);
                            disallowedKeys.Add(currentNumber);
                        }

                        // numbers is now properly ordered
                        if (isCorrupt)
                        {
                            middleValue = Convert.ToInt32(numbers[middle]);
                        }

                        sum += middleValue;
                    }
                }
            }

            return sum;
        }
    }
}
