using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingSheep
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFileName = args.First();
            var numbers = File.ReadAllLines(inputFileName);
            var sheep = new TheSheep();
            var results = sheep.ComputeSleepNumbers(numbers);
            File.WriteAllLines(inputFileName + ".results", results);
        }
    }
}
