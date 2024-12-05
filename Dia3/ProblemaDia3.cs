using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2024.Dia3;
public static class ProblemaDia3
{
    public static void ResolverParte1(string data)
    {
        string inputStr = File.ReadAllText(data);
        int totalSum = 0;
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        MatchCollection matches = Regex.Matches(inputStr, pattern);
        foreach (Match match in matches)
        {
            int x = int.Parse(match.Groups[1].Value);
            int y = int.Parse(match.Groups[2].Value);

            totalSum += x * y;
        }

        Debug.WriteLine(totalSum);
        Console.WriteLine(totalSum);
    }

    public static void ResolverParte2(string data)
    {
        int totalSum = 0;
        bool mulEnabled = true;
        string inputStr = File.ReadAllText(data);
        string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)", doPattern = @"do\(\)", dontPattern = @"don't\(\)";
        string combinedPattern = $"{mulPattern}|{doPattern}|{dontPattern}";
        MatchCollection matches = Regex.Matches(inputStr, combinedPattern);
        foreach (Match match in matches)
        {
            if (mulEnabled && match.Value.StartsWith("mul"))
            {
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                totalSum += x * y;
            }
            else if (match.Value == "do()")
            {
                mulEnabled = true;
            }
            else if (match.Value == "don't()")
            {
                mulEnabled = false;
            }
        }
        Debug.WriteLine(totalSum);
        Console.WriteLine(totalSum);
    }
}