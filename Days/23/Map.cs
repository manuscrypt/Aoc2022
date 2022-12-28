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

    public void AddElf(Point point)
    {
        Elves.Add(point, new Elf(point, this));
    }

    public bool Tick()
    {
        var proposals = new Dictionary<Elf, Point>();
        foreach (var elf in Elves.Values)
        {
            var proposal = elf.ProposePosition();
            if (proposal != null)
            {
                proposals.Add(elf, proposal);
            }
        }
        if (!proposals.Any())
        {
            return false;
        }
        //each elf moves to the proposed position, if they were the only elf to propose it
        var finalProps = proposals
            .GroupBy(x => x.Value)
            .Where(x => x.Count() == 1)
            .ToDictionary(x => x.Key, x => x.First().Key);
        foreach (var kv in finalProps)
        {
            MoveElf(kv.Value, kv.Key);
        }
        //Finally, at the end of the round, the first direction the Elves considered is moved to the end of the list of directions.
        Rotate();

        return true;
    }
    public void MoveElf(Elf elf, Point proposal)
    {
        if (!Elves.Remove(elf.Pos))
        {
            throw new ArgumentException("found no elf at pos: " + elf.Pos);
        }
        elf.Move(proposal);
        Elves.Add(elf.Pos, elf);
    }

    public IEnumerable<Elf> GetInDirection(Direction dir, Point pos)
    {
        foreach (var newPos in dir.GetDeltas().Select(delta => pos + delta))
        {
            if (Elves.TryGetValue(newPos, out var elf))
            {
                yield return elf;
            }
        }
    }

    public long CountEmptyTiles()
    {
        var count = 0;
        MapMap(pos =>
        {
            if (!Elves.ContainsKey(pos))
            {
                count++;
            }
        });
        return count;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var (min, max) = GetExtent();
        for (var i = min.Y; i <= max.Y; i++)
        {
            for (var j = min.X; j <= max.X; j++)
            {
                var p = new Point(j, i);
                sb.Append(!Elves.ContainsKey(p) ? "." : "#");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public void MapMap(Action<Point> action)
    {
        var (min, max) = GetExtent();

        for (var i = min.X; i <= max.X; i++)
        {
            for (var j = min.Y; j <= max.Y; j++)
            {
                action(new Point(i, j));
            }
        }
    }

    private (Point, Point) GetExtent()
    {
        var minX = Elves.Keys.Min(e => e.X);
        var maxX = Elves.Keys.Max(e => e.X);
        var minY = Elves.Keys.Min(e => e.Y);
        var maxY = Elves.Keys.Max(e => e.Y);
        return (new Point(minX, minY), new Point(maxX, maxY));
    }
}