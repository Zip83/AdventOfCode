using AdventOfCode.Shared;

namespace AdventOfCode._2019;

public class Day01 : Day
{
    public static void Main()
    {
        var instance = new Day01();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public override long GetTask1Result(string[] input)
    {
        var sum = 0L;
        foreach (var line in input)
        {
            var number = int.Parse(line);
            var result = (long)Math.Floor(number / 3.0) - 2;
            sum += result;
        }

        return sum;
    }

    public override long GetTask2Result(string[] input)
    {
        throw new NotImplementedException();
    }
}
