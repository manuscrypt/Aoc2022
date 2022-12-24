namespace Aoc2022.Days._17;

[DebuggerDisplay("Pos={Pos}")]
public class Piece
{
    public Point Pos { get; set; } = new(0, 0);
    public List<Point> Pixels { get; set; } = new();
    public List<Point> AbsolutePixels => Pixels.Select(p => Pos + p).ToList();

    public Piece(List<Point> pixels)
    {
        Pixels = pixels;
    }

    public bool CollidesWith(Piece other)
    {
        foreach (var myPixel in Pixels)
        {
            foreach (var otherPixel in other.Pixels)
            {
                if ((Pos + myPixel).Equals(other.Pos + otherPixel))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public long Top => Pixels.Max(p => Pos.Y + p.Y);
    public long Bottom => Pixels.Min(p => Pos.Y + p.Y);
    public long Left => Pixels.Min(p => Pos.X + p.X);
    public long Right => Pixels.Max(p => Pos.X + p.X);
    public bool Halted { get; set; }

    
}