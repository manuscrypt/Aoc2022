namespace Aoc2022.Days._1;

public class Solver
{
    public static async Task Solve()
    {
        var sums = await GetSums();
        SolveA(sums);
        SolveB(sums);
    }

    public static void SolveA(IEnumerable<int> sums)
    {
        Console.WriteLine(sums.Max());
    }
    public static void SolveB(IEnumerable<int> sums)
    {
        Console.WriteLine(sums.OrderByDescending(x => x).Take(3).Sum());
    }

    private static async Task<List<int>> GetSums()
    {
        var input = await File.ReadAllTextAsync(Path.Combine("Days", "1", "input.txt"));
        var groups = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var sums = groups.Select(g => g.Split(Environment.NewLine).Select(x => Convert.ToInt32(x)).Sum());
        return sums.ToList();
    }

}