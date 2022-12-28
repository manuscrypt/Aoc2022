namespace Aoc2022.Days._24;

public class ShortestDistances
{
    public static int Dfs(Map map, Point current, Point end, int offset, int time, Dictionary<(Point, int), int> memo)
    {
        //Console.WriteLine($"Current: {current} at t={time}");
        if (current.Equals(end))
        {
            return time;
        }
        if (memo.TryGetValue((current, time), out var value))
        {
            return value;
        }
        if (time > 350)
        {
            return int.MaxValue;
        }
        var neighbors = map.GetNeighbors(current, offset + time + 1).ToList();
        if (!neighbors.Any())
        {
            return int.MaxValue;
        }

        var min = int.MaxValue;
        foreach (var n in neighbors)
        {
            var d = Dfs(map, n, end, offset, time + 1,  memo);
            if (d < min)
            {
                min = d;
            }
        }

        memo.Add((current, time), min);

        return min;
    }
}