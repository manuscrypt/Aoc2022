namespace Aoc2022.Days._04;
public class Solver
{
    public static async Task Solve()
    {
        var input = await File.ReadAllLinesAsync(Path.Combine("Days", "4", "input.txt"));
        var assignments = input.Select(Assignment.FromString).ToList();
        SolveA(assignments);
        SolveB(assignments);
    }

    private static void SolveA(IEnumerable<Assignment> assignments)
    {
        Console.WriteLine(assignments.Count(a => a.FullyOverlaps()));
    }
    private static void SolveB(IEnumerable<Assignment> assignments)
    {
        Console.WriteLine(assignments.Count(a => a.Overlaps()));
    }
}
internal record Range(int From, int To)
{
    internal static Range FromString(string s)
    {
        var parts = s.Split('-');
        return new Range(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]));
    }

    internal IEnumerable<int> AsRange() => Enumerable.Range(From, To - From + 1);

    internal bool FullyOverlaps(Range other)
    {
        return From <= other.From && To >= other.To;
    }

    internal bool Overlaps(Range other)
    {
        return AsRange()
            .Intersect(other.AsRange())
            .Any();
    }
}

internal record Assignment(Range Left, Range Right)
{
    internal static Assignment FromString(string s)
    {
        var parts = s.Split(",");
        return new Assignment(Range.FromString(parts[0]), Range.FromString(parts[1]));
    }
    internal bool FullyOverlaps() => Left.FullyOverlaps(Right) || Right.FullyOverlaps(Left);
    internal bool Overlaps() => Left.Overlaps(Right);
}
