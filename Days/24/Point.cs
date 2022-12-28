namespace Aoc2022.Days._24;

[DebuggerDisplay("x = {X} y = {Y}")]
public class Point : IEquatable<Point>
{
    public Point(long x, long y)
    {
        X = x;
        Y = y;
    }

    public long X { get; set; }
    public long Y { get; set; }

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.X + b.X, a.Y + b.Y);
    }
    public long ManhattanDistance(Point other)
    {
        return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }
    public bool Equals(Point? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Point)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"x = {X} y = {Y}";
    }
}