using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2024.Dia1
{
    public static class ProblemaDia1
    {
        public static void ResolverParte1(string input)
        {
            string inputStr = File.ReadAllText(input);
            string[] lines = inputStr.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            List<int> startNumbers = [];
            List<int> endNumbers = [];
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    startNumbers.Add(int.Parse(parts[0]));
                    endNumbers.Add(int.Parse(parts[1]));
                }
            }
            startNumbers.Sort();
            endNumbers.Sort();
            int totalDistance = startNumbers.Zip(endNumbers, (start, end) => Math.Abs(start - end)).Sum();

            Console.WriteLine(totalDistance);
        }
        public static void ResolverParte2(string input)
        {
            string inputStr = File.ReadAllText(input);
            string[] lines = inputStr.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            List<int> startNumbers = [];
            List<int> endNumbers = [];
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    startNumbers.Add(int.Parse(parts[0]));
                    endNumbers.Add(int.Parse(parts[1]));
                }
            }
            var endNumberCounts = endNumbers.GroupBy(x => x).ToDictionary(group => group.Key, group => group.Count());
            int totalSum = startNumbers.Sum(start => start * (endNumberCounts.TryGetValue(start, out int value) ? value : 0));
            Console.WriteLine(totalSum);
        }
    }
}
