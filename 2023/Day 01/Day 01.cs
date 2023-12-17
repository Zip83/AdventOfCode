using System.Text.RegularExpressions;
using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day01 : Day
{
    public static void Main()
    {
        var instance = new Day01();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    private const string Task1NumberPattern = "([0-9])";

    public override long GetTask1Result(string[] input)
    {
        var sum = 0L;

        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line.Trim()))
            {
                continue;
            }

            var matchFirst = Regex.Match(line, Task1NumberPattern);
            var matchLast = Regex.Match(line, Task1NumberPattern, RegexOptions.RightToLeft);
            var joined = $"{matchFirst.Value}{matchLast.Value}";

            sum += int.Parse(joined);
        }

        return sum;
    }

    private const string Task2NumberPattern = "([0-9]|one|two|three|four|five|six|seven|eight|nine)";

    private static readonly Dictionary<string, int> NumberTable = new()
    {
        { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 },
        { "eight", 8 }, { "nine", 9 }
    };

    public override long GetTask2Result(string[] input)
    {
        var sum = 0L;

        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line.Trim()))
            {
                continue;
            }
            // Process line
            //Console.WriteLine(line);

            var matchFirst = Regex.Match(line, Task2NumberPattern);
            //Console.WriteLine($"FIRST NUMBER: {matchFirst.Value}");

            var matchLast = Regex.Match(line, Task2NumberPattern, RegexOptions.RightToLeft);
            //Console.WriteLine($"LAST NUMBER: {matchLast.Value}");

            var joined = $"{ConvertToNumbers(matchFirst.Value)}{ConvertToNumbers(matchLast.Value)}";

            sum += int.Parse(joined);
        }

        return sum;
    }

    private static int ConvertToNumbers(string numberString)
    {
        return int.TryParse(numberString, out var number)
            ? number
            : NumberTable.First(pair => pair.Key == numberString).Value;
    }
}
