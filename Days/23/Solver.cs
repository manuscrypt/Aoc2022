namespace Aoc2022.Days._23;

public class Solver
{
    public static async Task Solve()
    {
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "23", "input.txt"));

        var map = new Map();

        var y = 0;
        foreach (var line in lines)
        {
            var x = 0;
            foreach (var c in line)
            {
                if (c == '#')
                {
                    map.AddElf(new Point(x, y));
                }

                x++;
            }
            y++;
        }

        SolveA(map);
        SolveB(map);
    }

    private static void SolveA(Map map)
    {
        for (var i = 0; i < 10; i++)
        {
            Tick(map);
        }
        var emptyTiles = map.CountEmptyTiles();
        Console.WriteLine($"Found {emptyTiles} empty tiles on map");
    }
    private static void SolveB(Map map)
    {
        var round = 1;
        while (Tick(map))
        {
            round++;
        }
        Console.WriteLine($"No elves moved in round {round}");
    }

    private static bool Tick(Map map)
    {
        var proposals = new Dictionary<Elf, Point>();
        foreach (var elf in map.Elves.Values)
        {
            var proposal = elf.ProposePosition();
            if(proposal != null){
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
            map.MoveElf(kv.Value, kv.Key);
        }
        //Finally, at the end of the round, the first direction the Elves considered is moved to the end of the list of directions.
        map.Rotate();

        return true;
    }
}