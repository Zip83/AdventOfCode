using AdventOfCode.Shared;

namespace AdventOfCode._2019;

public class Day09 : Day
{
    public static void Main()
    {
        var instance = new Day09();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public static List<List<int>> ReadInput(string[] lines)
    {
        var input = new List<List<int>>();

        foreach (var line in lines)
        {
            var history = line.Split(' ').Select(int.Parse).ToList();
            input.Add(history);
        }

        return input;
    }

    public override long GetTask1Result(string[] input)
    {
        var sumOfExtrapolatedValues = ExtrapolateNextAndSum(ReadInput(input));
        Console.WriteLine("Sum of Extrapolated Values: " + sumOfExtrapolatedValues);

        return sumOfExtrapolatedValues;
    }

    static int ExtrapolateNextAndSum(List<List<int>> input)
    {
        return input.Sum(ExtrapolateNextValue);
    }

    static int ExtrapolateNextValue(List<int> history)
    {
        var stack = new Stack<int>();
        var differences = GetDifferences(history);
        stack.Push(differences.Last());

        while (differences.Count > 1 && differences.Any(diff => diff != 0))
        {
            differences = GetDifferences(differences);
            stack.Push(differences.Last());
        }

        return history.Last() + stack.Sum();
    }

    public static List<int> GetDifferences(List<int> sequence)
    {
        var differences = new List<int>();

        for (var i = 1; i < sequence.Count; i++)
        {
            differences.Add(sequence[i] - sequence[i - 1]);
        }

        return differences;
    }

    public override long GetTask2Result(string[] input)
    {
        var sumOfExtrapolatedValues = ExtrapolatePreviousAndSum(ReadInput(input));

        Console.WriteLine("Sum of Extrapolated Values: " + sumOfExtrapolatedValues);

        return sumOfExtrapolatedValues;
    }

    static int ExtrapolatePreviousAndSum(List<List<int>> input)
    {
        return input.Sum(ExtrapolatePreviousValue);
    }

    static int ExtrapolatePreviousValue(List<int> history)
    {
        var diffStack = new Stack<int>();
        var firstValueStack = new Stack<int>();
        var differences = GetDifferences(history);
        firstValueStack.Push(history.First());
        diffStack.Push(differences.First());

        while (differences.Count > 1 && differences.Any(diff => diff != 0))
        {
            var first = differences.First();
            firstValueStack.Push(first);
            differences = GetDifferences(differences);
            diffStack.Push(differences.First());
        }

        var diff = diffStack.Pop();
        foreach (var value in firstValueStack)
        {
            diff = value - diff;
        }

        return diff;
    }
}
