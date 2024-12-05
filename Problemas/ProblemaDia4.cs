using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2024.Problemas;
public static class ProblemaDia4
{
    public static void ResolverParte1(string data)
    {
        string[] lines = File.ReadAllLines(data);
        char[][] grid = lines.Select(line => line.ToCharArray()).ToArray();
        int occurrences = CountXMASOccurrencesParallel(grid);
        Debug.WriteLine(occurrences);
    }

    public static void ResolverParte2(string data)
    {
        string[] lines = File.ReadAllLines(data);
        char[][] grid = lines.Select(line => line.ToCharArray()).ToArray();
        int occurrences = CountXMASShapesParallel(grid);
        Debug.WriteLine(occurrences);
    }

    private static readonly (int, int)[] Directions =
    [
        (-1, 0), (1, 0), (0, -1), (0, 1), // Vertical y horizontal
        (-1, -1), (-1, 1), (1, -1), (1, 1) // Diagonal
    ];

    public static int CountXMASOccurrencesParallel(char[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        string word = "XMAS";
        int wordLength = word.Length;
        int totalOccurrences = 0;
        // Usar un objeto de bloqueo para garantizar la seguridad de los hilos al actualizar la variable compartida
        object lockObj = new();
        Parallel.For(0, rows, row =>
        {
            int localCount = 0;
            for (int col = 0; col < cols; col++)
            {
                foreach (var direction in Directions)
                {
                    if (CanSearch(row, col, direction, wordLength, rows, cols) &&
                        FindWord(grid, row, col, direction, word))
                    {
                        localCount++;
                    }
                }
            }
            lock (lockObj)
            {
                totalOccurrences += localCount;
            }
        });

        return totalOccurrences;
    }

    private static int CountXMASShapesParallel(char[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int totalShapes = 0;
        object lockObject = new();
        Parallel.For(1, rows - 1, row =>
        {
            int localCount = 0;

            for (int col = 1; col < cols - 1; col++)
            {
                if (IsValidXMASShape(grid, row, col))
                {
                    localCount++;
                }
            }
            lock (lockObject)
            {
                totalShapes += localCount;
            }
        });
        return totalShapes;
    }

    private static bool IsValidXMASShape(char[][] grid, int row, int col)
    {
        char topLeft = grid[row - 1][col - 1];
        char topRight = grid[row - 1][col + 1];
        char center = grid[row][col];
        char bottomLeft = grid[row + 1][col - 1];
        char bottomRight = grid[row + 1][col + 1];
        bool firstMAS = (topLeft == 'M' && center == 'A' && bottomRight == 'S') ||
                        (topLeft == 'S' && center == 'A' && bottomRight == 'M');

        bool secondMAS = (topRight == 'M' && center == 'A' && bottomLeft == 'S') ||
                         (topRight == 'S' && center == 'A' && bottomLeft == 'M');

        return firstMAS && secondMAS;
    }

    private static bool CanSearch(int row, int col, (int, int) direction, int wordLength, int rows, int cols)
    {
        int endRow = row + direction.Item1 * (wordLength - 1);
        int endCol = col + direction.Item2 * (wordLength - 1);
        return endRow >= 0 && endRow < rows && endCol >= 0 && endCol < cols;
    }

    private static bool FindWord(char[][] grid, int startRow, int startCol, (int, int) direction, string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            int row = startRow + direction.Item1 * i;
            int col = startCol + direction.Item2 * i;
            if (grid[row][col] != word[i])
            {
                return false;
            }
        }
        return true;
    }

}