namespace Aoc2022.Days._19;

public class Dfs
{
    public int Search(Blueprint blueprint, Node root, Dictionary<Node, int> memo, int max)
    {
        root.Visited = true;

        if (root.TimeRemaining == 0)
        {
            return root.Resources[ResourceType.Geode];
        }
        if (root.TimeRemaining == 1)
        {
            return root.Resources[ResourceType.Geode] + root.Robots[ResourceType.Geode];
        }
        if (root.TimeRemaining == 2 && !root.CanAfford(blueprint.GeodeRobotType, root.Resources))
        {
            return root.Resources[ResourceType.Geode] + (root.Robots[ResourceType.Geode] * root.TimeRemaining);
        }
        if (memo.TryGetValue(root, out var memoized))
        {
            return memoized;
        }
        foreach (var neighbor in root.GetNeighbors(blueprint))
        {
            if (!neighbor.Visited)
            {
                var geodes = Search(blueprint, neighbor, memo, max);
                
                if (geodes > max)
                {
                    max = geodes;
                }
            }
        }
        memo.Add(root, max);

        return max;
    }
}