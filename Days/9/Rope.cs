namespace Aoc2022.Days._9;

internal class Rope
{
    public LinkedList<Pos> Knots { get; set; }
    public List<Pos> TailPositions { get; set; } = new();

    internal Rope(int knots)
    {
        Knots = new LinkedList<Pos>(Enumerable.Range(0, knots).Select(_ => new Pos(0,0)).ToArray());
    }

    private void AddTailPositions(Pos pos)
    {
        TailPositions.Add(new Pos(pos.X, pos.Y));
    }

    public void Step(string dir)
    {
        var head = Knots.First();
        switch (dir)
        {
            case "U":
                head.Up();
                break;
            case "D":
                head.Down();
                break;
            case "L":
                head.Left();
                break;
            case "R":
                head.Right();
                break;
        }

        UpdateKnotsAndRecordTailPos();
    }

    public void UpdateKnotsAndRecordTailPos()
    {
        var head = Knots.First;
        while (head != null)
        {
            var tail = head.Next;
            if (tail == null)
            {
                break;
            }

            var dx = head.Value.X - tail.Value.X;
            var dy = head.Value.Y - tail.Value.Y;
            if (Math.Abs(dx) > 1)
            {
                tail.Value.Y = head.Value.Y;
                tail.Value.X = head.Value.X + Math.Sign(dx) * -1;
            }
            if (Math.Abs(dy) > 1)
            {
                tail.Value.X = head.Value.X;
                tail.Value.Y = head.Value.Y + Math.Sign(dy) * -1;
            }

            head = tail;
        }

        //Console.WriteLine($"H:{H} - T:{T}");
        if (!TailPositions.Any(tp => tp.Is(Knots.Last())))
        {
            AddTailPositions(Knots.Last());
        }
    }
}