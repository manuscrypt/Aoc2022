using System.Text.RegularExpressions;

namespace Aoc2022.Days._19;


public class Solver
{
    public static async Task Solve()
    {
        var input = "sample";
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "19", $"{input}.txt"));
        var bps = lines.Select(Parse).ToList();
        //SolveA(bps);
        SolveB(bps);
    }

    private static void SolveA(List<Blueprint> blueprints)
    {
        var traversal = new Dfs();

        var maxes = new List<int>();
        foreach (var blueprint in blueprints)
        {
            var memo = new Dictionary<Node, int>();
            var resources = new ResourceDict()
            {
                { ResourceType.Ore, 2 },
                { ResourceType.Clay, 0 },
                { ResourceType.Obsidian, 0 },
                { ResourceType.Geode, 0 },
            };
            var robots = new ResourceDict()
            {
                { ResourceType.Ore, 1 },
                { ResourceType.Clay, 0 },
                { ResourceType.Obsidian, 0 },
                { ResourceType.Geode, 0 },
            };
            var max = traversal.Search(blueprint, new Node(resources, robots, 22), memo, 0);
            maxes.Add(blueprint.Id * max);
            memo.Clear();
            memo = null;
            Console.WriteLine($"Quality-Level of blueprint {blueprint.Id} is {max}");
        }

        Console.WriteLine(maxes.Sum());
    }
    private static void SolveB(List<Blueprint> blueprints)
    {
        //takes too long like this
    }

    private static Blueprint Parse(string input)
    {
        var blueprintMatches = Regex.Matches(input, @"Blueprint (\d+):(.*)");
        Blueprint? bp = null;

        foreach (Match blueprintMatch in blueprintMatches)
        {
            var id = int.Parse(blueprintMatch.Groups[1].Value);
            bp = new Blueprint(id);
            var blueprintText = blueprintMatch.Groups[2].Value;

            var robotMatches = Regex.Matches(blueprintText, @"Each (\w+) robot costs (\d+) (\w+)(?: and (\d+) (\w+))?");
            foreach (Match robotMatch in robotMatches)
            {
                var kind = ToResourceType(robotMatch.Groups[1].Value);
                var cost1 = new Cost(ToResourceType(robotMatch.Groups[3].Value), int.Parse(robotMatch.Groups[2].Value));
                Cost? cost2 = null;
                if (robotMatch.Groups[4].Success)
                {
                    cost2 = new Cost(ToResourceType(robotMatch.Groups[5].Value), int.Parse(robotMatch.Groups[4].Value));
                }
                var robotType = new RobotType(kind, cost1, cost2);
                bp.AddRobotType(robotType);
            }
        }

        if (bp == null)
        {
            throw new Exception("parse error");
        }
        return bp;
    }

    private static ResourceType ToResourceType(string value)
    {
        return value.ToLowerInvariant() switch
        {
            "ore" => ResourceType.Ore,
            "clay" => ResourceType.Clay,
            "obsidian" => ResourceType.Obsidian,
            "geode" => ResourceType.Geode,
            _ => throw new ArgumentException("no such resource kind: " + value)
        };
    }
}