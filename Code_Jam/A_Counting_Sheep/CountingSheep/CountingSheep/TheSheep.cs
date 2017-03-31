using System;
using System.Collections.Generic;
using System.Linq;

namespace CountingSheep
{
    internal class TheSheep
    {
        public IList<string> ComputeSleepNumbers(string[] numbers)
        {
            var sleepNumbers = new List<string>();
            var count = numbers.First().ToInt32();

            foreach (var number in numbers.Skip(1).Take(count).Select(x=>Convert.ToInt64(x)))
            {
                if (number == 0)
                    sleepNumbers.Add("Case #{0}: INSOMNIA".FormatWith(sleepNumbers.Count + 1));
                else
                {
                    var numberToCount = number;
                    var switches = new int[10];
                    long upgrade = 1;

                    while (!switches.TrueForAll(x => x>0))
                    {
                        numberToCount =  number*upgrade;
                        var numberAsString = numberToCount.ToString();
                        for (int i = 0; i <= 9; i++)
                        {
                            if (numberAsString.Contains(i.ToString())) switches[i]++;
                        }
                        upgrade++;
                    }
                    sleepNumbers.Add("Case #{0}: {1}".FormatWith(sleepNumbers.Count + 1, numberToCount));
                }
            }
            return sleepNumbers;
        }
    }
}