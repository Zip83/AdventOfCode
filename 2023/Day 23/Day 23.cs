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

    private Graph CreateGraph(HikingTrail[][] map, Point starOfPath, Point endOfPath)
    {
        var graph = new Graph();
        graph.AddNode(starOfPath);
        for (var row = 0; row < map.Length; row++)
        {
            for (var col = 0; col < map[row].Length; col++)
            {
                var point = new Point(row, col);
                var hikingTrail = map[row][col];
                if (hikingTrail == HikingTrail.Forest)
                {
                    continue;
                }

                var adjacent = 0;
                foreach (var (_, d) in Direction)
                {
                    var newPoint = point + d;
                    if (IsValidPoint(ref map, newPoint))
                    {
                        adjacent++;
                    }
                }

                if (adjacent > 2)
                {
                    graph.AddNode(point);
                }
            }
        }

        graph.AddNode(endOfPath);
        // foreach (var (node, _) in graph.Nodes)
        // {
        //     Console.WriteLine(node);
        // }

        foreach (var (from, _) in graph.Nodes)
        {
            foreach (var (to, _) in graph.Nodes)
            {
                if (from == to)
                {
                    continue;
                }
                
                var pathVisited = new HashSet<Point>();
                var stack = new Stack<(Point Node, int Length)>();
                stack.Push((from, 0));
                while (stack.Count > 0)
                {
                    var (node, length) = stack.Pop();
                    pathVisited.Add(node);
                    if (graph.Nodes.ContainsKey(node) && node != from)
                    {
                        if (node == to)
                        {
                            graph.AddEdge(from, to, length);
                            break;
                        }
                        continue;
                    }

                    var hikingTrail = map[node.X][node.Y];
                    var direction = hikingTrail == HikingTrail.Path
                        ? Direction
                        : new Dictionary<HikingTrail, Point>
                        {
                            { hikingTrail, Direction[hikingTrail] }
                        };

                    foreach (var (_, d) in direction)
                    {
                        var newPoint = node + d;
                        if (!IsValidPoint(ref map, newPoint) || pathVisited.Contains(newPoint))
                        {
                            continue;
                        }

                        stack.Push((newPoint, length + 1));
                    }
                }
            }
        }

        // foreach (var ((from, to), weight) in graph.Edges)
        // {
        //     Console.WriteLine($"{from} => {to} >> {weight}");
        // }

        return graph;
    }

    private void DFS(Graph graph, Point u, Point v)
    {
        if (_visited.Contains(u))
        {
            return;
        }

        _visited.Add(u);
        _currentPath.Add(u);

        if (u == v)
        {
            _simplePaths.Add(_currentPath.ToArray());
            _visited.Remove(u);
            _currentPath.RemoveAt(_currentPath.Count - 1); // remove from back
            return;
        }

        foreach (var next in graph.Nodes[u])
        {
            DFS(graph, next, v);
        }

        _currentPath.RemoveAt(_currentPath.Count - 1); // remove from back
        _visited.Remove(u);
    }

    private readonly HashSet<Point> _visited = new();
    private readonly List<Point> _currentPath = new();
    private readonly List<Point[]> _simplePaths = new();

    public override long GetTask1Result(string[] input)
    {
        var map = ParseInput(input);
        var starOfPath = FindStartOfPath(ref map);
        var endOfPath = FindEndOfPath(ref map);

        var graph = CreateGraph(map, starOfPath, endOfPath);
        _visited.Clear();
        _currentPath.Clear();
        _simplePaths.Clear();
        DFS(graph, starOfPath, endOfPath);

        var maxLength = 0;
        foreach (var path in _simplePaths)
        {
            if (!path.Any())
            {
                break;
            }

            var pathLength = 0;
            var node = path.First();

            foreach (var next in path.Skip(1))
            {
                pathLength += graph.Edges[(node, next)];
                if (next == endOfPath)
                {
                    if (pathLength > maxLength)
                    {
                        maxLength = pathLength;
                    }

                    break;
                }

                node = next;
            }
        }

        return maxLength;
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