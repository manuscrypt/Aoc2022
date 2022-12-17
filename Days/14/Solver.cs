namespace Aoc2022.Days._14;

public class Solver
{
    public static async Task Solve()
    {
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "14", "input.txt"));
        var paths = ParseInput(lines);
        var pathPoints = ToPathPoints(paths);

        SolveA(pathPoints);

        var min = pathPoints.Min(p => p.X);
        var max = pathPoints.Max(p => p.X);
        var h = pathPoints.Max(p => p.Y) + 2;
        for (var i = min - 500; i < max + 500; i++)
        {
            pathPoints.Add(new Point(i, h));
        }

        SolveB(pathPoints);
    }

    private static void SolveA(HashSet<Point> parsedPaths)
    {
        var source = new Point(500, 0);
        var sands = new HashSet<Point>();

        while (true)
        {
            var newSand = new Point(source.X, source.Y);
            while (NextPos(ref newSand, sands, parsedPaths)){}
            if (IsAbyss(newSand, parsedPaths))
                break;
            sands.Add(newSand);
        }

        Console.WriteLine(sands.Count);
    }

    private static void SolveB(HashSet<Point> parsedPaths)
    {
        var source = new Point(500, 0);
        var sands = new HashSet<Point>();

        while (true)
        {
            // Add a new sand unit to the queue
            var newSand = new Point(source.X, source.Y);

            while (NextPos(ref newSand, sands, parsedPaths)) { }
            sands.Add(newSand);
            if (newSand.Equals(source))
                break;
        }

        Console.WriteLine(sands.Count);
    }

    private static bool NextPos(ref Point sand, HashSet<Point> sands, HashSet<Point> parsedPaths)
    {
        Point[] moves =
        {
            new(sand.X, sand.Y + 1),
            new(sand.X - 1, sand.Y + 1),
            new(sand.X + 1, sand.Y + 1)
        };
        foreach (Point move in moves)
        {
            if (IsOccupied(move, sands, parsedPaths))
            {
                continue;
            }
            sand = move;
            return !IsAbyss(sand, parsedPaths);
        }
        return false;
    }
    private static bool IsOccupied(Point point, IReadOnlySet<Point> sands, HashSet<Point> pathPoints) =>
        pathPoints.Contains(point) || sands.Contains(point);

    private static bool IsAbyss(Point lineStart, IReadOnlySet<Point> pathPoints)
    {
        var lineEnd = new Point(lineStart.X, lineStart.Y + 1000);
        for (var j = lineStart.Y; j < lineEnd.Y; j++)
        {
            var p = new Point(lineStart.X, j);
            if (pathPoints.Contains(p))
            {
                return false;
            }
        }
        return true;
    }
    private static HashSet<Point> ToPathPoints(List<List<Point>> paths)
    {
        var pathPoints = new HashSet<Point>();
        foreach (var path in paths)
        {
            for (var i = 0; i < path.Count - 1; i++)
            {
                var pathStart = path[i];
                var pathEnd = path[i + 1];

                var offX = Math.Sign(pathEnd.X - pathStart.X);
                var offY = Math.Sign(pathEnd.Y - pathStart.Y);
                var move = new Point(offX, offY);

                pathPoints.Add(pathStart);

                while (!pathStart.Equals(pathEnd))
                {
                    pathStart += move;
                    pathPoints.Add(pathStart);
                }
            }
        }

        return pathPoints;
    }
    private static List<List<Point>> ParseInput(string[] lines)
    {
        var paths = lines;

        var parsedPaths = new List<List<Point>>();

        foreach (var path in paths)
        {
            var points = path.Split(" -> ");

            var parsedPoints = new List<Point>();

            foreach (var point in points)
            {
                var coordinates = point.Split(',');

                var x = int.Parse(coordinates[0]);
                var y = int.Parse(coordinates[1]);

                parsedPoints.Add(new Point(x, y));
            }

            parsedPaths.Add(parsedPoints);
        }

        return parsedPaths;
    }
}