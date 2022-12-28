namespace Aoc2022.Days._23;

public static class DirectionExtensions
{

    private static readonly Dictionary<Direction, List<Point>> Deltas = new()
    {
        {
            Direction.North, new List<Point>
            {
                new(0, -1),
                new(1, -1),
                new(-1, -1)
            }
        },

        {
            Direction.South, new List<Point>
            {
                new(0, 1),
                new(1, 1),
                new(-1, 1)
            }
        },
        {
            Direction.West, new List<Point>
            {
                new(-1, 0),
                new(-1, -1),
                new(-1, 1)
            }
        },
        {
            Direction.East, new List<Point>
            {
                new(1, 0),
                new(1, -1),
                new(1, 1)
            }
        }
    };
    public static List<Point> GetDeltas(this Direction direction)
    {
        return Deltas[direction];
    }
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