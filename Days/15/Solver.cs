using System.Text.RegularExpressions;

namespace Aoc2022.Days._15;

public partial class Solver
{
    [GeneratedRegex("Sensor at x=(\\d+), y=(\\d+): closest beacon is at x=(-?\\d+), y=(-?\\d+)")]
    private static partial Regex MyRegex();

    public static async Task Solve()
    {
        //await Solve("sample", 10, 20);
        await Solve("input", 2000000, 4000000);
    }

    private static async Task Solve(string sampleOrInput, int lineA, int rangeB)
    {
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "15", $"{sampleOrInput}.txt"));
        var sensors = lines.Select(Parse).ToList();
        SolveA(sensors, lineA);
        SolveB(sensors, rangeB);
    }
    
    private static void SolveA(IEnumerable<Sensor> sensors, int fy)
    {
        var lines = GetLines(sensors, fy).OrderBy(l => l.From).ToList();
        var lMin = lines.Min(x => x.From.X);
        var lMax = lines.Max(x => x.To.X);
        Console.WriteLine(lMax - lMin);
    }

    private static void SolveB(IReadOnlyCollection<Sensor> sensors, int max)
    {
        var found = false;
        for (long y = 0; y < max; y++)
        {
            var lines = GetLines(sensors, y);
            for (long x = 0; x < max; x++)
            {
                var x1 = x;
                var l = lines.Where(l => x1 >= l.From.X && x1 <= l.To.X)
                    .MaxBy(l => l.To);
                if (l != null)
                {
                    x = l.To.X;
                }
                else
                {
                    Console.WriteLine($"x = {x}, y = {y} => {x * 4000000 + y}");
                    found = true;
                    break;
                }
            }

            if (found) break;
        }
    }
    
    private static List<Line> GetLines(IEnumerable<Sensor> sensors, long fy)
    {
        var relevantSensors = sensors.Where(s => Math.Abs(fy - s.Pos.Y) <=
                                                 s.Distance).ToList();

        return (from relevantSensor in relevantSensors.OrderBy(x => x.Pos) 
            let dy = Math.Abs(fy - relevantSensor.Pos.Y) 
            let rest = Math.Abs(relevantSensor.Distance - dy) 
            select new Line(
                new Point(relevantSensor.Pos.X - rest, fy), 
                new Point(relevantSensor.Pos.X + rest, fy)))
            .ToList();
    }

    private static readonly Regex Regex = MyRegex();

    private static Sensor Parse(string input)
    {
        var match = Regex.Match(input);
        if (!match.Success) throw new ArgumentException("no match");
        var sensorX = long.Parse(match.Groups[1].Value);
        var sensorY = long.Parse(match.Groups[2].Value);
        var beaconX = long.Parse(match.Groups[3].Value);
        var beaconY = long.Parse(match.Groups[4].Value);
        return new Sensor(new Point(sensorX, sensorY), new Point(beaconX, beaconY));
    }

}