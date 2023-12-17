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

    private List<string[]> ParseInput(string[] input)
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

        return blocks;
    }

    public override long GetTask1Result(string[] input)
    {
        var blocks = ParseInput(input);

        var result = 0L;
        foreach (var list in blocks)
        {
            result += TryFindReflection(list, false);
        }
        return result;
    }

    private long TryFindReflection(string[] block, bool useSmudge)
    {
        var result = TryFindHorizontalReflection(block, useSmudge);
        if (result == 0)
        {
            // Console.WriteLine("Try find vertical reflection.");
            // Console.WriteLine();
            result = TryFindVerticalReflection(block, useSmudge);
        }
        else
        {
            result *= 100;
        }

        return result;
    }

    private static int GetLineDiff(string row1, string row2)
    {
        var diff = 0;
        for (var i = 0; i < row1.Length; i++)
        {
            if (row1[i] != row2[i])
            {
                diff++;
            }
        }

        return diff;
    }

    private long TryFindHorizontalReflection(string[] block, bool useSmudge)
    {
        // foreach (var s in block)
        // {
        //     Console.WriteLine(s);
        // }
        // Console.WriteLine();
        
        var result = 0;
        for (var i = 0; i < block.Length; i++)
        {
            var row1 = block[i];
            if (i + 1 >= block.Length)
            {
                continue;
            }
            
            var row2 = block[i + 1];
            var diff = GetLineDiff(row1, row2);
            if (useSmudge && diff > 1)
            {
                continue;
            } 
            if (!useSmudge && diff > 0)
            {
                continue;
            }

            var smuggedUsed = false;
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
                diff = GetLineDiff(checkRow1, checkRow2);
                if (useSmudge)
                {
                    if (diff == 0)
                    {
                        continue;
                    } 
                    
                    if (diff == 1 && !smuggedUsed)
                    {
                        smuggedUsed = true;
                        continue;
                    }
                    
                    isReflection = false;
                    break;
                } 
                
                if (diff > 0)
                {
                    isReflection = false;
                    break;
                }
            }

            if (isReflection)
            {
                if (useSmudge && !smuggedUsed)
                {
                    continue;
                }
                
                // Console.WriteLine($"Reflection found at {i + 1} / {i + 2}.");
                // Console.WriteLine();
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

    private long TryFindVerticalReflection(string[] block, bool useSmudge)
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

        return TryFindHorizontalReflection(transposed.Select(pair => pair.Value.ToString()).ToArray(), useSmudge);
    }

    public override long GetTask2Result(string[] input)
    {
        var blocks = ParseInput(input);

        var result = 0L;
        foreach (var list in blocks)
        {
            result += TryFindReflection(list, true);
        }
        return result;
    }
}
