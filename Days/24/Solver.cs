namespace Aoc2022.Days._24;

public class Solver
{
    public static async Task Solve()
    {
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "24", "input.txt"));
        //SolveA(ParseMap(lines));
        SolveB(ParseMap(lines));
    }

    private static void SolveA(Map map)
    {
        var memo = new Dictionary<(Point, int), int>();
        var sd = ShortestDistances.Dfs(map, map.Start, map.End, 0, 0, memo);
        Console.WriteLine(sd);
    }

    private static void SolveB(Map map)
    {
        var memo = new Dictionary<(Point, int), int>();

        var sd1 = ShortestDistances.Dfs(map, map.Start, map.End, 0, 0, memo);
        Console.WriteLine($"Trip 1 took {sd1} minutes. now at {map.End}");

        memo = new Dictionary<(Point, int), int>();
        var sd2 = ShortestDistances.Dfs(map, map.End, map.Start, sd1, 0, memo);
        Console.WriteLine($"Trip 2 took {sd2} minutes. now at {map.Start}");

        memo = new Dictionary<(Point, int), int>();
        var sd3 = ShortestDistances.Dfs(map, map.Start, map.End, sd1 + sd2, 0, memo);
        Console.WriteLine($"Trip 3 took {sd3} minutes. now at {map.End}");
        
        Console.WriteLine(sd1 + sd2 + sd3);
    }
    private static Map ParseMap(string[] lines)
    {
        var map = new Map(lines[0].Length, lines.Count());
        var y = 0;
        foreach (var line in lines)
        {
            var x = 0;
            foreach (var c in line)
            {
                map.Init(c, x, y);
                x++;
            }

            y++;
        }
        return map;
    }

}
/*
 *         
        //var fromNode = graph.GetOrCreateNode(start, 0);
        //var startNodes = new List<Node> { fromNode };
        //var t = 1;
        //foreach (var map in maps)
        //{
        //    var nextNodes = new List<Node>();
        //    var distinctStarts = startNodes.Distinct().ToList();
        //    foreach (var startNode in distinctStarts)
        //    {
        //        foreach (var dir in Blizzard.MoveDirections)
        //        {
        //            var np = startNode.Point + dir;
        //            if (map.PointOnMap(np))
        //            {
        //                var mo = map.MapObjects.Where(x => !x.IsOpenSpace)
        //                    .FirstOrDefault(x => x.Pos.Equals(np));
        //                if (mo == null)
        //                {
        //                    var nn = graph.GetOrCreateNode(np, t);
        //                    nextNodes.Add(nn);
        //                }
        //            }
        //        }
        //    }

        //    t++;
        //    if (!nextNodes.Any())
        //    {
        //        nextNodes = new List<Node>(startNodes);
        //    }
        //    startNodes = new List<Node>(nextNodes);
        //}

*/