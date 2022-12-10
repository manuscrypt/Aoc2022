namespace Aoc2022.Days._09;

internal class Rope
{
    public LinkedList<Pos> Knots { get; set; }
    public List<Pos> TailPositions { get; set; } = new();

    internal Rope(int knots)
    {
        Knots = new LinkedList<Pos>(Enumerable.Range(0, knots).Select(_ => new Pos(0, 0)).ToArray());
        AddTailPositions(Knots.Last());
    }

    private void AddTailPositions(Pos pos)
    {
        if (TailPositions.All(tp => !tp.Equals(pos)))
        {
            TailPositions.Add(new Pos(pos.X, pos.Y));
        }
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
        if(head == null) return;
        var tail = head.Next;
        while (tail != null && head != tail)
        {
            var dp = new Pos(head.Value.X - tail.Value.X, head.Value.Y - tail.Value.Y);
            var xMoved = false;
            if (Math.Abs(dp.X) > 1)
            {
                tail.Value.Y = head.Value.Y;
                tail.Value.X = head.Value.X + Math.Sign(dp.X) * -1;
                xMoved = true;
            } 
            if (Math.Abs(dp.Y) > 1)
            {
                if(!xMoved) tail.Value.X = head.Value.X;
                tail.Value.Y = head.Value.Y + Math.Sign(dp.Y) * -1;
            }

            head = tail;
            tail = head.Next;
        }

        AddTailPositions(Knots.Last());
    }
}