using System.Drawing;
using System.Drawing.Imaging;

namespace Aoc2022.Days._9;

public class Solver
{
    public static async Task Solve()
    {
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "9", "input.txt"));
        var moves = lines.Select(l => l.Split(' '))
            .Select(parts => new Move(parts[0], Convert.ToInt32(parts[1])))
            .ToList();
        SolveA(moves,2);
        SolveA(moves,10);

    }

    private static void SolveA(IList<Move> moves, int knots)
    {
        var rope = new Rope(knots);
        foreach (var move in moves)
        {
            for (var i = 0; i < move.Amt; i++)
            {
                rope.Step(move.Dir);
            }
        }

        Console.WriteLine(rope.TailPositions.Count);
        CreateImage(rope, $"rope-{knots}");
    }

    static void CreateImage(Rope rope, string name)
    {
        var minX = rope.TailPositions.Min(p => p.X);
        var dimX = rope.TailPositions.Max(p => p.X) - minX;
        var minY = rope.TailPositions.Min(p => p.Y);
        var dimY = rope.TailPositions.Max(p => p.Y) - minY;
        // Create a new Bitmap object with the desired width and height
        var width = (dimX + 1) * 10;
        var height = (dimY + 1) * 10;
        var bmp = new Bitmap(width, height);

        // Use a Graphics object to draw on the Bitmap
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.FillRectangle(Brushes.White, 0,0, width, height);
            foreach (var tp in rope.TailPositions)
            {
                // Set the color to use for the corners
                g.FillRectangle(Brushes.Red, (tp.X - minX) * 10, (tp.Y - minY) * 10, 10, 10);
            }
        }

        // Save the Bitmap to a file
        bmp.Save($"{name}.png", ImageFormat.Png);
    }
}


