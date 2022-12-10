namespace Aoc2022.Days._10;

public class Solver
{
    public static async Task Solve()
    {
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "10", "input.txt"));
        var p = new Processor(20, 60, 100, 140, 180, 220);
        p.Process(lines);

        SolveA(p);
        SolveB(p);
    }

    private static void SolveA(Processor p)
    {
        Console.WriteLine(p.X);
        Console.WriteLine(p.Result());
    }
    private static void SolveB(Processor p)
    {
        foreach (var line in p.CrtLines)
        {
            foreach (var x in line)
            {
                Console.Write(x ? "#" : ".");
            }
            Console.WriteLine();
        }
    }
}