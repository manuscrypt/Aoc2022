namespace Aoc2022.Days._9;

internal class Pos 
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

    public bool Is(Pos pos)
    {
        return X == pos.X && Y == pos.Y;
    }
}