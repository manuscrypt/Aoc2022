namespace Aoc2022.Days._24;

public class Blizzard
{
    public static HashSet<char> Blizzards = new() { '<', '>', 'v', '^' };
    public static Dictionary<char, Point> BlizzardDirections = new()
    {
        {'^',new Point(0, -1)},
        {'v',new Point(0, 1)},
        {'<',new Point(-1, 0)},
        {'>',new Point(1, 0)}
    };

    public char C { get; }
    public Point Pos { get; set; }

    public Blizzard(char c, Point pos)
    {
        C = c;
        Pos = pos;
    }
}