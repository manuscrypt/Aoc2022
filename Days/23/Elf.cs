namespace Aoc2022.Days._23;

[DebuggerDisplay("Pos={Pos}")]
public class Elf
{
    private Map Map { get; }
    public Point Pos { get; set; }
    public Elf(Point pos, Map map)
    {
        Pos = pos;
        Map = map;
    }
    public Point? ProposePosition()
    {
        var aroundMe = Map.CurrentDirectionOrder
            .ToDictionary(dir => dir, dir => Map.GetInDirection(dir, Pos).ToList());

        if (!aroundMe.Values.SelectMany(x => x).Any())
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