namespace Aoc2022.Days._17;

[DebuggerDisplay("X={X}, Y={Y}")]

public class Point : IEquatable<Point>
{
    public long X { get; set; }
    public long Y { get; set; }

    public Point(Point p)
    {
        X = p.X;
        Y = p.Y;
    }

    public Point(long x, long y)
    {
        X = x;
        Y = y;
    }

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.X + b.X, a.Y + b.Y);
    }

    public bool Equals(Point? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y;
    }
}