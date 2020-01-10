using System;
using System.IO;
using Solution.Domain;
using Solution.Repository;

namespace SolutionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IProposalRepository repository = new ProposalRepository();
            string basePath = $"../../test";

            for (var index = 0; index < 13; index++)
            {
                var strIndex = index.ToString().PadLeft(3, '0');
                var inputLines = File.ReadAllLines($"{basePath}/input/input{strIndex}.txt");
                var outputLines = File.ReadAllLines($"{basePath}/output/output{strIndex}.txt");

                var result = Solution.Instance(repository).ProcessMessages(inputLines);
                if (outputLines[0] == result)
                    Console.WriteLine($"Test #{index + 1}/#13 - Passed");
                else
                    Console.WriteLine($"Test #{index + 1}/#13 - Failed");
            }
        }
    }
}
