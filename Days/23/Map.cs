using System.Text;

namespace Aoc2022.Days._23;

public class Map
{
    public Dictionary<Point, Elf> Elves { get; set; } = new();

    public List<Direction> CurrentDirectionOrder = new()
    {
        Direction.North, Direction.South, Direction.West, Direction.East
    };


    public void Rotate()
    {
        var first = CurrentDirectionOrder.First();
        CurrentDirectionOrder.RemoveAt(0);
        CurrentDirectionOrder.Add(first);
    }

    private readonly Dictionary<Direction, List<Point>> _deltas = new()
    {
        {
            Direction.North, new List<Point>
            {
                new Point(0, -1),
                new Point(1, -1),
                new Point(-1, -1)
            }
        },

        {
            Direction.South, new List<Point>
            {
                new Point(0, 1),
                new Point(1, 1),
                new Point(-1, 1)
            }
        },
        {
            Direction.West, new List<Point>
            {
                new Point(-1, 0),
                new Point(-1, -1),
                new Point(-1, 1)
            }
        },
        {
            Direction.East, new List<Point>
            {
                new Point(1, 0),
                new Point(1, -1),
                new Point(1, 1)
            }
        }
    };
    public void AddElf(Point point)
    {
        Elves.Add(point,new Elf(point, this));
    }
    public void MoveElf(Elf kvKey, Point proposal)
    {
        if (!Elves.Remove(kvKey.Pos))
        {
            throw new ArgumentException("found no elf at pos: " + kvKey.Pos);
        }
        kvKey.Move(proposal);
        Elves.Add(kvKey.Pos, kvKey);
    }

    public IEnumerable<Elf> GetInDirection(Direction dir, Point pos)
    {
        var deltas = _deltas[dir];
        foreach (var delta in deltas)
        {
            var newPos = pos + delta;
            if (Elves.TryGetValue(newPos, out var elf))
            {
                yield return elf;
            }
        }
    }

    public long CountEmptyTiles()
    {
        var minX = Elves.Keys.Min(e => e.X);
        var maxX = Elves.Keys.Max(e => e.X);
        var minY = Elves.Keys.Min(e => e.Y);
        var maxY = Elves.Keys.Max(e => e.Y);
        var count = 0;
        for (var i = minX; i <= maxX; i++)
        {
            for (long j = minY; j <= maxY; j++)
            {
                var p = new Point(i, j);
                if (!Elves.ContainsKey(p))
                {
                    count++;
                }
            }
        }
        return count;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var minX = Elves.Keys.Min(e => e.X);
        var maxX = Elves.Keys.Max(e => e.X);
        var minY = Elves.Keys.Min(e => e.Y);
        var maxY = Elves.Keys.Max(e => e.Y);
        for (var i = minY; i <= maxY; i++)
        {
            for (long j = minX; j <= maxX; j++)
            {
                var p = new Point(j, i);
                sb.Append(!Elves.ContainsKey(p) ? "." : "#");
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

}