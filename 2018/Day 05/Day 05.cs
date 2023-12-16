using AdventOfCode.Shared;

namespace Day_05;

public class Day05 : Day
{
    private const int From = 65; // A
    private const int To = 90; // Z

    public static void Main()
    {
        var instance = new Day05();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public override long GetTask1Result(string[] input)
    {
        var line = input.First();
        var l = line.Length;
        do
        {
            for (var i = 0; i < l - 1; i++)
            {
                l = line.Length;
                var c1 = line[i];
                var c2 = line[i + 1];
                if (c1 != c2 && c1.ToString().ToUpper() == c2.ToString().ToUpper())
                {
                    // Console.WriteLine("Removing");
                    line = line.Remove(i, 2);
                    i -= 2;
                    if (i < -1)
                    {
                        i = -1;
                    }
                }
            }
        } while(l != line.Length);

        return line.Length;
    }

    public override long GetTask2Result(string[] input)
    {
        var line2 = input.First();
        var min = Int32.MaxValue;
        for (var ch = From; ch <= To; ch++)
        {
            var line = new string(line2.ToCharArray());
            var c = Convert.ToChar(ch);
            // Console.WriteLine(c);
            line = line.Replace(c.ToString(), "");
            line = line.Replace(c.ToString().ToLower(), "");
            // Console.WriteLine(line);
            var l = line.Length;
            do
            {
                for (var i = 0; i < line.Length - 1; i++)
                {
                    l = line.Length;
                    var c1 = line[i];
                    var c2 = line[i + 1];
                    if (c1 != c2 && c1.ToString().ToUpper() == c2.ToString().ToUpper())
                    {
                        // Console.WriteLine("Removing");
                        line = line.Remove(i, 2);
                        i -= 2;
                        if (i < -1)
                        {
                            i = -1;
                        }
                    }
                }
            } while(l != line.Length);

            // Console.WriteLine(line);
            if (line.Length < min)
            {
                min = line.Length;
            }
        }

        return min;
    }
}
