using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day23 : Day
{
    private static readonly Dictionary<HikingTrail, Point> Direction = new()
    {
        { HikingTrail.SlopeDown, new Point(1, 0) },
        { HikingTrail.SlopeLeft, new Point(0, -1) },
        { HikingTrail.SlopeUp, new Point(-1, 0) },
        { HikingTrail.SlopeRight, new Point(0, 1) }
    };

    public static void Main()
    {
        var instance = new Day23();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    private HikingTrail[][] ParseInput(string[] input)
    {
        var rows = input.Length;
        var cols = input[0].Length;
        var map = new HikingTrail[rows][];

        for (var row = 0; row < input.Length; row++)
        {
            map[row] = new HikingTrail[cols];
            var line = input[row];

            for (var col = 0; col < line.Length; col++)
            {
                var hikingTrail = (HikingTrail)line[col];
                map[row][col] = hikingTrail;
            }
        }

        return map;
    }

    public Point FindStartOfPath(ref HikingTrail[][] map)
    {
        var row = 0;
        var col = FindEndPoint(map[row]);

        return new Point(row, col);
    }

    public Point FindEndOfPath(ref HikingTrail[][] map)
    {
        var row = map.Length - 1;
        var col = FindEndPoint(map[row]);

        return new Point(row, col);
    }

    private int FindEndPoint(HikingTrail[] line)
    {
        var col = 0;
        for (; col < line.Length; col++)
        {
            if (line[col] != HikingTrail.Forest)
            {
                break;
            }
        }

        return col;
    }

    public override long GetTask1Result(string[] input)
    {
        var map = ParseInput(input);
        var starOfPath = FindStartOfPath(ref map);
        var endOfPath = FindEndOfPath(ref map);

        var trailLength = 1;

        // point for inspection, length to the point, list of visited points
        var stack = new Stack<(Point, int, HikingTrail[][])>();
       
        var visitedPoints = Copy(map);

        stack.Push((starOfPath + Direction[HikingTrail.SlopeDown], 1, visitedPoints));
        while (stack.Any())
        {
            (var currentPoint, var lenght, visitedPoints) = stack.Pop();
            if (!IsValidPoint(ref map, currentPoint))
            {
                continue;
            }

            if (visitedPoints[currentPoint.X][currentPoint.Y] == HikingTrail.Visited)
            {
                continue; // already visited, every point can be visited only once
            }

            if (currentPoint == endOfPath)
            {
                if (lenght > trailLength)
                {
                    trailLength = lenght;
                    PrintHikingTrail(visitedPoints);
                }

                continue;
            }

            visitedPoints[currentPoint.X][currentPoint.Y] = HikingTrail.Visited;
            switch (map[currentPoint.X][currentPoint.Y])
            {
                case HikingTrail.Forest:
                case HikingTrail.Visited:
                    continue;
                case HikingTrail.Path:
                    stack.Push((currentPoint + Direction[HikingTrail.SlopeDown], lenght + 1, Copy(visitedPoints)));
                    stack.Push((currentPoint + Direction[HikingTrail.SlopeRight], lenght + 1, Copy(visitedPoints)));
                    stack.Push((currentPoint + Direction[HikingTrail.SlopeLeft], lenght + 1, Copy(visitedPoints)));
                    stack.Push((currentPoint + Direction[HikingTrail.SlopeUp], lenght + 1, Copy(visitedPoints)));
                    break;
                default:
                    var hikingTrail = map[currentPoint.X][currentPoint.Y];
                    stack.Push((currentPoint + Direction[hikingTrail], lenght + 1, Copy(visitedPoints)));
                    break;
            }
        }

        return trailLength;
    }

    private static HikingTrail[][] Copy(HikingTrail[][] map)
    {
        var copy = new HikingTrail[map.Length][];
        for (var i = 0; i < map.Length; i++)
        {
            var line = map[i];
            copy[i] = new HikingTrail[line.Length];
            Array.Copy(line, copy[i], line.Length);
        }

        return copy;
    }

    private static void PrintHikingTrail(HikingTrail[][] map)
    {
        foreach (var line in map)
        {
            Console.WriteLine(string.Join("", line.Select(i => (char)(int)i).ToList()));
        }
        Console.WriteLine();
    }
    
    string RemoveSlopes(string st) =>
        string.Join("", st.Select(ch => ">v<^".Contains(ch) ? '.' : ch));

    public override long GetTask2Result(string[] input)
    {
        return GetTask1Result(input.Select(RemoveSlopes).ToArray());
    }

    private static bool IsValidPoint(ref HikingTrail[][] map, Point point)
    {
        if (point.X < 0 || point.X >= map.Length)
        {
            return false;
        }

        return point.Y >= 0 && point.Y < map[0].Length && map[point.X][point.Y] != HikingTrail.Forest;
    }
}