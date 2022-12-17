namespace Aoc2022.Days._15;

public class Line
{
    public Line(Point from, Point to)
    {
        From = from;
        To = to;
    }

    public Point From { get; set; }
    public Point To { get; set; }
}