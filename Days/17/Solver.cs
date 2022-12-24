namespace Aoc2022.Days._17;

public class Solver
{
    public static async Task Solve()
    {
        var input = "sample";
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "17", $"{input}.txt"));
        var stream = lines.Single().ToList();
        SolveA(stream, 2022);
        //SolveA(stream, 1000000000000);
    }

    private static void SolveA(List<char> stream, long steps)
    {
        var chamber = new Chamber(stream);
        var pf = new PieceFactory();
        for (var i = 0; i < steps; i++)
        {
            chamber.Spawn(pf.Next());
        }
        Console.WriteLine(chamber.Height + 1);
        //Console.WriteLine((double) ((double)(chamber.Height + 1) / (double)(steps)));
    }
}