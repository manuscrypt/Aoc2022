namespace Aoc2022.Days._18;

internal class Cube
{
    public bool IsFilled { get; set; }
    public Point3d Pos { get; }
    public int SidesExposed { get; set; } = 6;
    public Cube(Point3d pos, bool isFilled)
    {
        Pos = pos;
        IsFilled = isFilled;
    }
}