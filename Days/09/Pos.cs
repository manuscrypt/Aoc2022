namespace Aoc2022.Days._09;

internal class Pos : IEquatable<Pos>
{
    public Pos(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public void Right()
    {
        X += 1;
    }
    public void Left()
    {
        X -= 1;
    }
    public void Down()
    {
        Y += 1;
    }
    public void Up()
    {
        Y -= 1;
    }

    public override string ToString()
    {
        return $"{X}/{Y}";
    }

    public bool Equals(Pos? other)
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
        return Equals((Pos)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}