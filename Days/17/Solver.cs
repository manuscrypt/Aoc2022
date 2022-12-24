using System.Drawing;

namespace Aoc2022.Days._17;

public class Solver
{
    public static async Task Solve()
    {
        var input = "sample";
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "17", $"{input}.txt"));
        var stream = lines.Single().ToList();
        SolveA(stream, 10000);
        //SolveA(stream, 200000);
        //SolveA(stream, 1000000000000);
    }

    private static void SolveA(List<char> stream, long steps)
    {
        var chamber = new Chamber(stream);
        var pf = new PieceFactory();
        var sw = Stopwatch.StartNew();

        var tops = new List<List<long>>();

        for (var i = 0; i < steps; i++)
        {
            var piece = pf.Next();
            chamber.Spawn(piece);

            var topList = new List<long>();
            for (var x = 0; x < 7; x++)
            {
                long top = 0;
                var thisX = chamber.Pieces.SelectMany(p => p.AbsolutePixels).Where(p => p.X == x).ToList();
                if (thisX.Any())
                {
                    top = thisX.Max(p => p.Y);
                }
                var dt = chamber.Height - top;
                topList.Add(dt);
            }

            if (tops.Any(t => t.SequenceEqual(topList)))
            {
                Console.WriteLine($"Found a repeat at step {i}");
            }

            //chamber.Pieces.RemoveAll(p => p.Top < chamber.Height - 9);
            //chamber.Draw();
            //if (piece.Pixels.SequenceEqual(PieceFactory.LineH))
            //{
            //    Console.WriteLine($"H={(chamber.Height + 1)} -- {stream[chamber.StreamPos]}");
            //}
        }

        foreach (var tl in tops)
        {
            Console.WriteLine(string.Join(",", tl));
        }

        sw.Stop();
        //Console.WriteLine((double) ((double)(chamber.Height + 1) / (double)(steps)));
    }
}