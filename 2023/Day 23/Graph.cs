using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Graph
{
    public readonly Dictionary<Point, List<Point>> Nodes = new();
    public readonly Dictionary<(Point From, Point To), int> Edges = new();

    public void AddNode(Point node)
    {
        if (!Nodes.ContainsKey(node))
        {
            Nodes.Add(node, new List<Point>());
        }
    }

    public void AddEdge(Point from, Point to, int weight = 0)
    {
        if (!Nodes.ContainsKey(from))
        {
            Nodes.Add(from, new List<Point> { to });
        }

        if (!Nodes[from].Contains(to))
        {
            Nodes[from].Add(to);
        }
        
        if (!Nodes.ContainsKey(to))
        {
            Nodes.Add(to, new List<Point>());
        }

        Edges.TryAdd((from, to), weight);
    }

    public bool TryUpdateEdge(Point from, Point to, int weight)
    {
        if (!Edges.ContainsKey((from, to)))
        {
            return false;
        }
        
        Edges[(from, to)] = weight;
        return true;

    }
}