using System.Reactive.Linq;

namespace Aoc2022.Days._6;

public class Solver
{
    public static async Task Solve()
    {
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "6", "input.txt"));
        foreach (var line in lines)
        {
            SolveA(line);
            SolveB(line);
        }
    }

    private static void SolveA(string line)
    {
        Solve(line, 4);
    }
    private static void SolveB(string line)
    {
        Solve(line, 14);
    }

    private static void Solve(string line, int count)
    {
        var query = line.ToObservable()
            .Buffer(count, 1)
            .Select(x => new HashSet<char>(x))
            .Where(x => x.Count == count)
            .Select(x => string.Join(string.Empty, x))
            .FirstAsync();

        using var subscribe = query.Subscribe(marker =>
        {
            Console.WriteLine(line.IndexOf(marker, StringComparison.OrdinalIgnoreCase) + count);
        });
    }
}


