namespace Aoc2022.Days._17;

public class PieceFactory
{
    public long Counter { get; private set; }

    public PieceFactory()
    {
        Counter = 0;
    }

    public Piece Next()
    {
        var piece = new Piece(All[(int)(Counter % All.Count)]);
        Counter++;
        return piece;
    }

    public static List<Point> LineH = new()
    {
        new Point(0, 0),
        new Point(1, 0),
        new Point(2, 0),
        new Point(3, 0),
    };
    public static List<Point> Cross = new()
    {
        new Point(1, 2),
        new Point(0, 1),
        new Point(1, 1),
        new Point(2, 1),
        new Point(1, 0),
    };
    public static List<Point> L = new()
    {
        new Point(2, 2),
        new Point(2, 1),
        new Point(0, 0),
        new Point(1, 0),
        new Point(2, 0),
    };
    public static List<Point> LineV = new()
    {
        new Point(0, 3),
        new Point(0, 2),
        new Point(0, 1),
        new Point(0, 0),
    };
    public static List<Point> Square = new()
    {
        new Point(0, 1),
        new Point(1, 1),
        new Point(0, 0),
        new Point(1, 0),
    };
    public static List<List<Point>> All = new()
    {
        LineH, Cross, L, LineV, Square
    };  
}