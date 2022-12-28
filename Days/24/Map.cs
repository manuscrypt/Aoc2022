namespace Aoc2022.Days._24;

public class Map
{
    public int Width { get; }
    public int Height { get; }
    public static List<Point> MoveDirections = new()
    {
        new Point(0, -1),
        new Point(0, 1),
        new Point(-1, 0),
        new Point(1, 0 )
    };

    public readonly Point Start;
    public readonly Point End;


    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Start = new Point(1, 0);
        End = new Point(Width - 2, Height - 1);
    }

    public Dictionary<int, List<Blizzard>> Blizzards { get; set; } = new();

    public void Init(char c, int x, int y)
    {
        if (c != '.' && c != '#')
        {
            if (!Blizzards.TryGetValue(0, out var blizzards))
            {
                blizzards = new List<Blizzard>();
                Blizzards.Add(0, blizzards);
            }

            blizzards.Add(new Blizzard(c, new Point(x, y)));
        }
    }

    public bool PointOnMap(Point potPos)
    {
        return (potPos.X > 0 && potPos.X < Width - 1 && potPos.Y > 0 && potPos.Y < Height - 1) 
            || potPos.Equals(Start)
            || potPos.Equals(End);
    }

    public List<Blizzard> MoveBlizzards(int t)
    {
        var blizzards = Blizzards[t - 1].Select(MoveBlizzard).ToList();
        return blizzards;
    }

    private Blizzard MoveBlizzard(Blizzard blizzard)
    {
        var direction = Blizzard.BlizzardDirections[blizzard.C];
        var next = blizzard.Pos + direction;
        if (next.X <= 0) next.X = Width - 2;
        if (next.X >= Width - 1) next.X = 1;
        if (next.Y <= 0) next.Y = Height - 2;
        if (next.Y >= Height - 1) next.Y = 1;
        return new Blizzard(blizzard.C, next);
    }

   
    public IEnumerable<Point> GetNeighbors(Point current, int time)
    {
        if (!Blizzards.TryGetValue(time, out var blizzards))
        {
            blizzards = MoveBlizzards(time);
            Blizzards.Add(time, blizzards);
        };
        foreach (var direction in MoveDirections)
        {
            var next = current + direction;
            if (PointOnMap(next))
            {
                var mo = blizzards.Where(x => x.Pos.Equals(next)).ToList();
                if (!mo.Any())
                {
                    yield return next;
                }
            }
        }

        if (!blizzards.Any(x => x.Pos.Equals(current)))
        {
            yield return current;
        }
    }
}