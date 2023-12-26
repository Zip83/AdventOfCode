using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day15 : Day
{
    public static void Main()
    {
        var instance = new Day15();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public static int Hash(string str)
    {
        var hash = 0;
        foreach (var c in str)
        {
            var asciiCode = (int)c; // Determine the ASCII code for the current character of the string
            hash += asciiCode; // Increase the current value by the ASCII code you just determined
            hash *= 17; // Set the current value to itself multiplied by 17.
            hash %= 256; // Set the current value to the remainder of dividing itself by 256.
        }

        return hash;
    }

    public override long GetTask1Result(string[] input)
    {
        var line = input.First();
        return line.Split(",").Sum(Hash);
    }

    public override long GetTask2Result(string[] input)
    {
        throw new NotImplementedException();
    }
}
