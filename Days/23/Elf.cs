namespace Aoc2022.Days._23;

[DebuggerDisplay("Pos={Pos}")]
public class Elf
{
    private Map Map { get; }
    public Point Pos { get; set; }

    public List<Direction> Directions { get; set; } = new()
    {
        Direction.North, Direction.South, Direction.West, Direction.East
    };

    public Elf(Point pos, Map map)
    {
        Pos = pos;
        Map = map;
    }

    public Point? ProposePosition()
    {
        var aroundMe = new Dictionary<Direction, List<Elf>>();
        foreach (var dir in Map.CurrentDirectionOrder)
        {
            aroundMe.Add(dir, Map.GetInDirection(dir, Pos).ToList());
        }
        var count = aroundMe.Values.SelectMany(x => x).Count();
        if (count == 0)
        {
            return null;
        }
        foreach (var dir in Map.CurrentDirectionOrder)
        {
            var elves = aroundMe[dir];
            if (elves.Count == 0)
            {
                return Pos + dir.ToPoint();
            }
        }

        return null;
    }

    public void Move(Point proposal)
    {
        Pos = proposal;
    }
}