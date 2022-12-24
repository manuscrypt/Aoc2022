using System.Text;

namespace Aoc2022.Days._17;

public class Chamber
{
    public Chamber(List<char> stream)
    {
        Stream = stream;
        StreamPos = 0;
    }

    public int StreamPos { get; set; }

    public long Width { get; set; } = 7;
    public long Height => Pieces.Max(x => x.Top);

    public List<Piece> Pieces { get; set; } = new List<Piece>();
    public List<char> Stream { get; set; }
    public void Spawn(Piece p)
    {
        p.Pos.X = 2;
        p.Pos.Y = !Pieces.Any() ? 3 : Pieces.Max(x => x.Top + 1) + 3;

        //Draw();
        //Console.ReadLine();

        var collided = false;
        while (!collided)
        {
            Drift(p);
            collided = Fall(p);
            p.Halted = true;
        }
        Pieces.Add(p);

    }

    private void Draw()
    {
        Console.Clear();
        var lines = new List<string> { "+-------+" };
        var pixels = Pieces.SelectMany(x => x.AbsolutePixels).ToList();
        for (var y = 0; y < Height + 1; y++)
        {
            var line = new StringBuilder();
            line.Append("|");
            var row = pixels.Where(x => x.Y == y).ToList();
            for (var x = 0; x < Width; x++)
            {
                var pixel = row.FirstOrDefault(p => p.X == x);
                line.Append(pixel == null ? "." : "#");
            }
            line.Append("|");
            lines.Add(line.ToString());
        }

        lines.Reverse();
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
        Thread.Sleep(32);
    }

    private bool Fall(Piece piece)
    {
        var originalPos = new Point(piece.Pos);
        //apply falling motion
        piece.Pos.Y -= 1;
        var collided = (DetectCollisionWithPieces(piece) || DetectCollisionWithFloor(piece));
        if(collided){
            piece.Pos = originalPos;
        }

        return collided;
    }

    private void Drift(Piece piece)
    {
        var originalPos = new Point(piece.Pos);
        //apply jet stream
        piece.Pos.X += Stream[StreamPos] == '<' ? -1 : 1;
        if (DetectCollisionWithPieces(piece) || DetectCollisionWithWalls(piece))
        {
            piece.Pos = originalPos;
        }

        StreamPos = (StreamPos + 1) % Stream.Count;

    }

    private bool DetectCollisionWithFloor(Piece piece)
    {
        return piece.Bottom < 0;
    }

    private bool DetectCollisionWithWalls(Piece piece)
    {
        return piece.Left < 0 || piece.Right >= Width;
    }

    private bool DetectCollisionWithPieces(Piece piece)
    {
        var piecesToCheck = Pieces.Where(other => other != piece)
            .Where(other => Math.Abs(other.Pos.Y - piece.Pos.Y) <= 8)
            .ToList();
        return piecesToCheck.Any(piece.CollidesWith);
    }
}