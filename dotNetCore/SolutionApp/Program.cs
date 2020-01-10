using System;
using System.IO;

namespace SolutionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string basePath = $"../test";

            for (var index = 0; index < 13; index++)
            {
                var strIndex = index.ToString().PadLeft(3,'0');
                var inputLines = File.ReadAllLines($"{basePath}/input/input{strIndex}.txt");
                var outputLines = File.ReadAllLines($"{basePath}/output/output{strIndex}.txt");

                var result = Solution.ProcessMessages(inputLines);
                if (outputLines[0] == result)
                    Console.WriteLine($"Test #{index + 1}/#13 - Passed");
                else
                    Console.WriteLine($"Test #{index + 1}/#13 - Failed");
            }
        }
    }
}
