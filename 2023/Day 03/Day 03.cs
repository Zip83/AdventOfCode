using System.Text;
using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day03 : Day
{
    public static void Main()
    {
        var instance = new Day03();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    private static bool IsValidPosition(string[] lines, int row, int col)
    {
        return row >= 0 && row < lines.Length && col >= 0 && col < lines[row].Length;
    }

    public override long GetTask1Result(string[] input)
    {
        var sum = 0;

        for (var i = 0; i < input.Length; i++)
        {
            var currentNumber = new StringBuilder();

            for (var j = 0; j < input[i].Length; j++)
            {
                if (char.IsDigit(input[i][j]))
                {
                    currentNumber.Append(input[i][j]);
                }
                else if (currentNumber.Length > 0)
                {
                    if (AnyDigitAdjacentToSymbol(input, i, j - 1, currentNumber))
                    {
                        sum += int.Parse(currentNumber.ToString());
                    }
                    currentNumber.Clear();
                }
            }

            // Check if there's a number at the end of the row
            if (currentNumber.Length > 0 && AnyDigitAdjacentToSymbol(input, i, input[i].Length - 1, currentNumber))
            {
                sum += int.Parse(currentNumber.ToString());
            }
        }

        return sum;
    }

    static bool AnyDigitAdjacentToSymbol(string[] schematic, int row, int col, StringBuilder currentNumber)
    {
        for (var k = 0; k < currentNumber.Length; k++)
        {
            if (IsAdjacentToSymbol(schematic, row, col - k))
            {
                return true;
            }
        }

        return false;
    }

    static bool IsAdjacentToSymbol(string[] schematic, int row, int col)
    {
        // Check all adjacent positions, including diagonals
        for (var i = row - 1; i <= row + 1; i++)
        {
            for (var j = col - 1; j <= col + 1; j++)
            {
                if (IsValidPosition(schematic, i, j) && !char.IsDigit(schematic[i][j]) && schematic[i][j] != '.')
                {
                    return true;
                }
            }
        }

        return false;
    }

    public override long GetTask2Result(string[] input)
    {
        var gears = FindGears(input);

        var sum = 0;

        // Find and print gear parts for each gear
        var gearPartsForGears = FindGearPartsForGears(input, gears);
        Console.WriteLine("Gear Parts for Each Gear:");
        foreach (var gearAndParts in gearPartsForGears)
        {
            Console.WriteLine(
                $"Gear at ({gearAndParts.Key.Item1}, {gearAndParts.Key.Item2}): {string.Join(", ", gearAndParts.Value)}");
            if (gearAndParts.Value.Count == 2)
            {
                sum += gearAndParts.Value[0] * gearAndParts.Value[1];
            }
        }

        return sum;
    }

    private static Dictionary<(int, int), List<int>> FindGearPartsForGears(string[] engineSchematic,
        List<(int, int)> gears)
    {
        var gearPartsForGears = new Dictionary<(int, int), List<int>>();

        foreach (var gear in gears)
        {
            var gearParts = FindGearParts(engineSchematic, gear.Item1, gear.Item2);
            gearPartsForGears.Add(gear, gearParts);
        }

        return gearPartsForGears;
    }

    private static List<(int, int)> FindGears(string[] engineSchematic)
    {
        var gears = new List<(int, int)>();

        for (var i = 0; i < engineSchematic.Length; i++)
        {
            for (var j = 0; j < engineSchematic[i].Length; j++)
            {
                if (engineSchematic[i][j] == '*')
                {
                    gears.Add((i, j));
                }
            }
        }

        return gears;
    }

    private static string GetConsecutiveDigits(string[] lines, int row, int col, HashSet<(int, int)> usedDigits)
    {
        var partString = lines[row][col].ToString();
        usedDigits.Add((row, col));

        // Check for consecutive digits from right to left
        for (var newCol = col - 1; newCol >= 0 && char.IsDigit(lines[row][newCol]); newCol--)
        {
            usedDigits.Add((row, newCol));
            partString = lines[row][newCol] + partString;
        }

        // Check for consecutive digits from left to right
        for (var newCol = col + 1; newCol < lines[row].Length && char.IsDigit(lines[row][newCol]); newCol++)
        {
            usedDigits.Add((row, newCol));
            partString += lines[row][newCol];
        }

        return partString;
    }

    private static List<int> FindGearParts(string[] lines, int row, int col)
    {
        var usedDigits = new HashSet<(int, int)>();
        var gearParts = new List<int>();

        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                var newRow = row + i;
                var newCol = col + j;

                if (IsValidPosition(lines, newRow, newCol) && char.IsDigit(lines[newRow][newCol]) && !usedDigits.Contains((newRow, newCol)))
                {
                    var consecutiveDigits = GetConsecutiveDigits(lines, newRow, newCol, usedDigits);
                    gearParts.Add(int.Parse(consecutiveDigits));
                }
            }
        }

        return gearParts;
    }
}
