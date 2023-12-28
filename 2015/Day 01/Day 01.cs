using AdventOfCode.Shared;

namespace AdventOfCode._2015;

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
        var line = input.First();
        var up = line.Count(c => c == '(');
        var down = line.Count(c => c == ')');

        return up - down;
    }

    public override long GetTask2Result(string[] input)
    {
        var floor = 0;
        var line = input.First();
        int position;
        for (position = 0; position < line.Length; position++)
        {
            var c = line[position];
            switch (c)
            {
                case '(':
                    floor++;
                    break;
                case ')':
                    floor--;
                    break;
            }

            if (floor == -1)
            {
                break;
            }
        }

        return position + 1;
    }
}