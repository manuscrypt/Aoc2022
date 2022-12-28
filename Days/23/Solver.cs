namespace Aoc2022.Days._23;

public class Solver
{
    public static async Task Solve()
    {
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "23", "input.txt"));
        SolveA(ParseMap(lines));
        SolveB(ParseMap(lines));
    }

    private static void SolveA(Map map)
    {
        for (var i = 0; i < 10; i++)
        {
            map.Tick();
        }
        var emptyTiles = map.CountEmptyTiles();
        Console.WriteLine($"Found {emptyTiles} empty tiles on map");
    }
    
    private static void SolveB(Map map)
    {
        var round = 1;
        while (map.Tick())
        {
            round++;
        }
        Console.WriteLine($"No elves moved in round {round}");
    }

    private static Map ParseMap(string[] lines)
    {
        var map = new Map();
        var points = lines.SelectMany((line, y) =>
        {
            return line.Select((c, x) => c == '#' ? new Point(x, y) : null)
                .Where(x => x != null).ToList();
        }).ToList();

        foreach (var p in points)
        {
            if (p != null)
            {
                map.AddElf(p);
            }
        }

        return map;
    }

}