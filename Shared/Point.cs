namespace AdventOfCode.Shared;

public struct Point
{
    public long X { get; }
    public long Y { get; }

    public Point(long x, long y)
    {
        X = x;
        Y = y;
    }
    
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Point a, Point b) => a.X != b.X || a.Y != b.Y;

    public override string ToString()
    {
        return $"{GetType().Name}({X}, {Y})";
    }
}
