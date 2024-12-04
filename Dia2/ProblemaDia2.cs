using System.Diagnostics;

namespace Aoc2024.Dia2;
public static class ProblemaDia2
{
    public static void ResolverParte1(string data)
    {
        string inputStr = File.ReadAllText(data);
        string[] reports = inputStr.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        int safeReports = 0;
        foreach (string report in reports)
        {
            string[] levels = report.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (IsAlwaysSafeReport(levels))
            {
                safeReports++;
            }
        }
        Debug.WriteLine(safeReports);
        Console.WriteLine(safeReports);
    }

    public static void ResolverParte2(string data)
    {
        string inputStr = File.ReadAllText(data);
        string[] reports = inputStr.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        int safeReports = 0;
        foreach (string report in reports)
        {
            string[] levels = report.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (IsSafeReport(levels))
            {
                safeReports++;
            }
        }
        Debug.WriteLine(safeReports);
        Console.WriteLine(safeReports);
    }

    private static bool IsSafeReport(string[] levels)
    {
        int[] levelNumbers = Array.ConvertAll(levels, int.Parse);
        if (IsAlwaysSafeReport(levels))
        {
            return true;
        }
        for (int i = 0; i < levelNumbers.Length; i++)
        {
            List<int> modifiedLevels = new(levelNumbers);
            modifiedLevels.RemoveAt(i);
            if (IsAlwaysSafeReport(modifiedLevels.Select(x => x.ToString()).ToArray()))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsAlwaysSafeReport(string[] levels)
    {
        int[] levelNumbers = Array.ConvertAll(levels, int.Parse);
        bool isIncreasing = true;
        bool isDecreasing = true;
        for (int i = 0; i < levelNumbers.Length - 1; i++)
        {
            int difference = levelNumbers[i + 1] - levelNumbers[i];
            if (difference < -3 || difference > 3 || difference == 0)
            {
                return false;
            }
            if (difference < 0)
            {
                isIncreasing = false;
            }
            if (difference > 0)
            {
                isDecreasing = false;
            }
        }
        return (isIncreasing || isDecreasing);
    }
}