namespace Aoc2022.Days._18;

[DebuggerDisplay("x={X},y={Y},z={Z}")]
internal class Point3d : IEquatable<Point3d>
{
    public Point3d(int[] coords)
    {
        X = coords[0];
        Y = coords[1];
        Z = coords[2];
    }

    public Point3d(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Point3d(Point3d coords)
    {
        X = coords.X;
        Y = coords.Y;
        Z = coords.Z;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public bool Equals(Point3d? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Point3d)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }
    public static Point3d operator +(Point3d a, Point3d b)
    {
        return new Point3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

}