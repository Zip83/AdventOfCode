using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day12 : Day
{
    public static void Main()
    {
        var instance = new Day12();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public override long GetTask1Result(string[] input)
    {
        var sum = 0L;

        var list = ReadInput(input);
        foreach (var (spring, conditions) in list)
        {
            sum += Ways(spring, conditions);
        }

        return sum;
    }

    public static List<(string Springs, int[] Condition)> ReadInput(string[] input)
    {
        var data = new List<(string Springs, int[] Condition)>();
        foreach (var line in input)
        {
            var parts = line.Split(" ");
            var springs = parts[0];
            var condition = parts[1].Split(",").Select(int.Parse).ToArray();

            data.Add((springs, condition));
        }

        return data;
    }

    public static long Ways(string pattern, int[] counts)
    {
        return Ways(pattern, counts, 0, 0, new Dictionary<(int, int), long>());
    }

    static long Ways(string pattern, int[] counts, int patternIndex, int countIndex, Dictionary<(int, int), long> waysMemo)
    {
        var memoKey = (patternIndex, countIndex);
        if (waysMemo.TryGetValue(memoKey, out var memo))
        {
            return memo;
        }

        if (patternIndex >= pattern.Length)
        {
            return countIndex == counts.Length ? 1 : 0;
        }

        long ways = 0;

        var currentPatternChar = pattern[patternIndex];
        if (currentPatternChar != '#') // either . so we skip it, or ? so consider what if we do skip it
        {
            ways += Ways(pattern, counts, patternIndex + 1, countIndex, waysMemo);
        }

        if (currentPatternChar != '.' && countIndex < counts.Length) // either # so we consume it, or ? so consider what if we do consume it
        {
            var enoughToConsume = true;
            var neededCount = counts[countIndex] - 1; // we've already got the first
            while (enoughToConsume && neededCount > 0)
            {
                patternIndex++;
                neededCount--;
                enoughToConsume = patternIndex < pattern.Length && pattern[patternIndex] != '.';
            }

            if (enoughToConsume)
            {
                var separatorIndex = patternIndex + 1;
                if (separatorIndex >= pattern.Length || pattern[separatorIndex] != '#')
                {
                    ways += Ways(pattern, counts, separatorIndex + 1, countIndex + 1, waysMemo);
                }
            }
        }

        waysMemo.Add(memoKey, ways);
        return ways;
    }

    public override long GetTask2Result(string[] input)
    {
        var sum = 0L;

        var list = ReadInput(input);
        foreach (var (spring, conditions) in list)
        {
            var expandedConditions = new List<int>();
            for (var i = 0; i < 5; i++)
            {
                expandedConditions.AddRange(conditions);
            }
            sum += Ways(string.Join("?", Enumerable.Repeat(spring, 5)), expandedConditions.ToArray());
        }

        return sum;
    }
}
