namespace Aoc2022.Days._10;

public class Processor
{
    private readonly int[] _partOneStops;
    private readonly List<int> _intermediates = new();
    private List<bool> _curLine = null!;

    public Processor(params int[] partOneStops)
    {
        _partOneStops = partOneStops;
        CycleNr = 0;
        X = 1;
    }

    public int CycleNr { get; set; }
    public int X { get; set; }
    public int CrtPos { get; set; }
    public List<List<bool>> CrtLines { get; } = new();

    public void Noop()
    {
        Cycle();
    }
    public void AddX(int v)
    {
        Cycle();
        Cycle();
        X += v;
    }

    private void Cycle()
    {
        CrtPos = CycleNr % 40;
        if (CrtPos == 0)
        {
            _curLine = new List<bool>();
            CrtLines.Add(_curLine);
        }
        CycleNr++;
        if (_partOneStops.Any(c => c == CycleNr))
        {
            _intermediates.Add(CycleNr * X);
        }

        var sprite = new List<int> { X - 1, X, X + 1 };
        _curLine.Add(sprite.Contains(CrtPos));
    }

    public int Result()
    {
        return _intermediates.Sum();
    }

    public void Process(string[] lines)
    {
        foreach (var line in lines)
        {
            if (line.Equals("noop"))
            {
                Noop();
            }
            else
            {
                var parts = line.Split(' ');
                AddX(Convert.ToInt32(parts[1]));
            }
        }
    }
}