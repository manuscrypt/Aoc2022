using Aoc2022.Days._09;
using System.Collections;

namespace Aoc2022.Days._22;

internal enum Label
{
    Front, Back, Left, Right, Top, Bottom
}

[DebuggerDisplay("Pos = {Pos}")]
internal class Face
{
    public Point Pos { get; }
    public List<Cell> Cells { get; } = new();
    public Label? Label { get; set; }

    public Face(Point pos)
    {
        Pos = pos;
    }

}

[DebuggerDisplay("Pos = {Pos}, c = {C}")]

internal class Cell
{
    public Point Pos { get; }
    public char C { get; }

    public Cell(Point pos, char c)
    {
        Pos = pos;
        C = c;
    }
}

[DebuggerDisplay("x = {X}, y = {Y}")]
public class Point : IComparable<Point>
{
    public int X { get; }
    public int Y { get; }
    public static Point operator +(Point a, Point b)
    {
        return new Point(a.X + b.X, a.Y + b.Y);
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point(a.X - b.X, a.Y - b.Y);
    }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public override string ToString()
    {
        return $"{X},{Y}";
    }

    public int CompareTo(Point? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        var yComp = Y.CompareTo(other.Y);
        if (yComp != 0) return yComp;
        return X.CompareTo(other.X);
    }
}