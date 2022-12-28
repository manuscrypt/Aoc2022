namespace Aoc2022.Days._23;

public static class DirectionExtensions
{
    public static Point ToPoint(this Direction dir)
    {
        return dir switch
        {
            Direction.North => new Point(0, -1),
            Direction.South => new Point(0, 1),
            Direction.West => new Point(-1, 0),
            Direction.East => new Point(1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
        };
    }
}