using System.Text;
using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day13 : Day
{
    public static void Main()
    {
        var instance = new Day13();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public override long GetTask1Result(string[] input)
    {
        var blocks = new List<string[]>();
        var block = new List<string>();
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line.Trim()))
            {
                blocks.Add(block.ToArray());
                block.Clear();
            }
            else
            {
                block.Add(line);
            }
        }

        if (block.Any())
        {
            blocks.Add(block.ToArray());
        }

        var result = 0L;
        foreach (var list in blocks)
        {
            result += TryFindReflection(list);
        }
        return result;
    }

    private long TryFindReflection(string[] block)
    {
        var result = TryFindHorizontalReflection(block);
        if (result == 0)
        {
            Console.WriteLine("Try find vertical reflection.");
            Console.WriteLine();
            result = TryFindVerticalReflection(block);
        }
        else
        {
            result *= 100;
        }

        return result;
    }

    private long TryFindHorizontalReflection(string[] block)
    {
        foreach (var s in block)
        {
            Console.WriteLine(s);
        }
        Console.WriteLine();
        
        var result = 0;
        for (var i = 0; i < block.Length; i++)
        {
            var row1 = block[i];
            if (i + 1 >= block.Length)
            {
                continue;
            }
            
            var row2 = block[i + 1];
            if (row1 != row2)
            {
                continue;
            }

            // check, if it is really reflection
            var isReflection = true;
            for (var k = 0; i - k >= 0; k++)
            {
                var checkRow1 = block[i - k];
                if (i + 1 + k >= block.Length)
                {
                    break;
                }

                var checkRow2 = block[i + 1 + k];
                if (checkRow1 != checkRow2)
                {
                    isReflection = false;
                    break;
                }
            }

            if (isReflection)
            {
                Console.WriteLine($"Reflection found at {i + 1} / {i + 2}.");
                Console.WriteLine();
                result = i + 1;
                break;
            }

            if (result > 0)
            {
                break;
            }
        }

        return result;
    }

    private long TryFindVerticalReflection(string[] block)
    {
        var transposed = new Dictionary<int, StringBuilder>();
        for (var i = 0; i < block.Length; i++)
        {
            var line = block[i];
            for (var j = 0; j < line.Length; j++)
            {
                if (!transposed.ContainsKey(j))
                {
                    transposed.Add(j, new StringBuilder());
                }

                transposed[j].Append(line[j]);
            }
        }

        return TryFindHorizontalReflection(transposed.Select(pair => pair.Value.ToString()).ToArray());
    }

    public override long GetTask2Result(string[] input)
    {
        throw new NotImplementedException();
    }
}
