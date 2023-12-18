using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day18 : Day
{
    public static void Main()
    {
        var instance = new Day18();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    private static List<Instruction> ParseInput(string[] input, bool useColor)
    {
        var instructions = new List<Instruction>();
        foreach (var line in input)
        {
            var split = line.Split(" ");

            if (useColor)
            {
                var color = split[2].Substring(2, split[2].Length - 3);
                var length = long.Parse(color[..5], System.Globalization.NumberStyles.HexNumber);
                var direction = (Direction)int.Parse(color[^1].ToString());
                instructions.Add(new Instruction(direction, length));
            }
            else
            {
                var direction = Enum.Parse<Direction>(split[0]);
                var length = long.Parse(split[1]);
                instructions.Add(new Instruction(direction, length));
            }
        }

        return instructions;
    }

    private List<Point> CreatePoints(Instruction[] instructions)
    {
        var points = new List<Point>();
        var previousPoint = new Point(0, 0);
        foreach (var instruction in instructions)
        {
            var point = instruction.Direction switch
            {
                Direction.R => new Point(previousPoint.X + instruction.Length, previousPoint.Y),
                Direction.L => new Point(previousPoint.X - instruction.Length, previousPoint.Y),
                Direction.D => new Point(previousPoint.X, previousPoint.Y + instruction.Length),
                _ => new Point(previousPoint.X, previousPoint.Y - instruction.Length)
            };

            points.Add(point);
            previousPoint = point;
        }

        return points;
    }

    /// <summary>
    /// The Shoelace Formula https://en.wikipedia.org/wiki/Shoelace_formula - Other formulas
    /// Area = 0.5 * SUM_i=1^n ( y_i * (x_i-1 - x_i+1) )
    /// </summary>
    /// <returns></returns>
    private long GetPolygonArea(List<Point> points)
    {
        var area = 0L;
        for (var i = 1; i <= points.Count; i++)
        {
            var previousPoint = points[(i - 1) % points.Count];
            var point = points[i % points.Count];
            var nextPoint = points[(i + 1) % points.Count];
            area += point.X * (nextPoint.Y - previousPoint.Y);
        }

        return Math.Abs(area) / 2L;
    }

    private long GetResult(string[] input, bool useColor)
    {
        var instructions = ParseInput(input, useColor).ToArray();
        var points = CreatePoints(instructions);

        var lagoonSize = GetPolygonArea(points) + instructions.Sum(instruction => instruction.Length) / 2 + 1;
        Console.WriteLine($"Lagoon total size: {lagoonSize}");
        return lagoonSize;
    }

    public override long GetTask1Result(string[] input)
    {
        return GetResult(input, false);
    }

    public override long GetTask2Result(string[] input)
    {
        return GetResult(input, true);
    }
}
