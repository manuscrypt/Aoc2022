using System.Reactive.Linq;

namespace Aoc2022.Days._6;

public class Solver
{
    public static async Task Solve()
    {
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "6", "input.txt"));
        foreach (var line in lines)
        {
            SolveA(line, 4);
            SolveA(line, 14);
        }
    }

    private static void SolveA(string line, int count)
    {
        var query = line.ToObservable()
            .Buffer(count, 1)
            .Where(l => l.Count == count);
        var marker = string.Empty;
        using var subscribe = query.Subscribe(i =>
        {
            if (i.Distinct().Count() == i.Count && marker == string.Empty)
            {
                marker = string.Join("", i);
            }
        });
        Console.WriteLine(line.IndexOf(marker, StringComparison.OrdinalIgnoreCase) + count);
    }
}


