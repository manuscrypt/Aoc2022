namespace Aoc2022.Days._18;
public class Solver
{
    private static readonly List<Point3d> AllDirs = new()
    {
        new Point3d(-1, 0, 0),
        new Point3d(+1, 0, 0),
        new Point3d(0, -1, 0),
        new Point3d(0, +1, 0),
        new Point3d(0, 0, -1),
        new Point3d(0, 0, +1),
    };

    public static async Task Solve()
    {
        var input = "input";
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "18", $"{input}.txt"));
        var points = ParsePoints(lines).ToList();
        var cubes = new Dictionary<Point3d, Cube>();
        foreach (var p in points)
        {
            var cube = new Cube(p, true);
            cubes.Add(cube.Pos, cube);
        }
        SolveA(cubes);
        cubes = new Dictionary<Point3d, Cube>();
        foreach (var p in points)
        {
            var cube = new Cube(p, true);
            cubes.Add(cube.Pos, cube);
        }
        SolveB(cubes);
    }

    private static void SolveB(Dictionary<Point3d, Cube> cubes)
    {
        var exposedSides = SolveA(cubes);
        var xMin = cubes.Keys.Min(x => x.X);
        var yMin = cubes.Keys.Min(x => x.Y);
        var zMin = cubes.Keys.Min(x => x.Z);
        var xMax = cubes.Keys.Max(x => x.X);
        var yMax = cubes.Keys.Max(x => x.Y);
        var zMax = cubes.Keys.Max(x => x.Z);
        var allCubes = new Dictionary<Point3d, Cube>();
        for (int x = -2 * xMin; x < 2 * xMax; x++)
        {
            for (int y = -2 * yMin; y < 2 * yMax; y++)
            {
                for (int z = -2 * zMin; z < 2 * zMax; z++)
                {
                    var p = new Point3d(x, y, z);
                    if (!cubes.TryGetValue(p, out var cube))
                    {
                        cube = new Cube(p, false);
                    }
                    allCubes.Add(p, cube);
                }
            }
        }
        FloodFill(allCubes, allCubes.Values.Last(x => !x.IsFilled));

        var notFilledCubes = allCubes.Select(c => c.Value).Where(c => !c.IsFilled).ToList();

        var a = SolveA(notFilledCubes.ToDictionary(x => x.Pos, x => x));
        Console.WriteLine("using a: {0}", exposedSides - a);
        var touchingSides = CountTouchingSides(notFilledCubes, cubes);
        Console.WriteLine("using touching: {0}", exposedSides - touchingSides);
    }

    private static int CountTouchingSides(List<Cube> notFilledCubes, Dictionary<Point3d, Cube> cubes)
    {
        int count = 0;
        foreach (var notFilledCube in notFilledCubes)
        {
            var p = notFilledCube.Pos;
            foreach (var dir in AllDirs)
            {
                var np = p + dir;
                if (cubes.ContainsKey(np))
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static void FloodFill(Dictionary<Point3d, Cube> cubes, Cube c)
    {
        var q = new Queue<Cube>();

        void Action(Point3d p)
        {
            if (!cubes.TryGetValue(p, out var cube)) return;
            if (cube.IsFilled) return;
            q.Enqueue(cube);
        }

        q.Enqueue(c);
        while (q.Count > 0)
        {
            var n = q.Dequeue();
            if (n.IsFilled) continue;
            n.IsFilled = true;
            foreach (var np in AllDirs.Select(dir => n.Pos + dir))
            {
                Action(np);
            }
        }
    }

    private static IEnumerable<Point3d> ParsePoints(string[] lines) =>
        lines.Select(l =>
            new Point3d(l.Split(",").Select(x => Convert.ToInt32(x)).ToArray()));

    private static int SolveA(Dictionary<Point3d, Cube> cubes)
    {
        foreach (var a in cubes.Keys)
        {
            var a1 = a;
            var adjacent = cubes.Keys.Where(b =>
            {
                var dx = Math.Abs(b.X - a1.X);
                var dy = Math.Abs(b.Y - a1.Y);
                var dz = Math.Abs(b.Z - a1.Z);
                return !b.Equals(a1) &&
                       ((dx == 1 && dy == 0 && dz == 0) ||
                        (dx == 0 && dy == 1 && dz == 0) ||
                        (dx == 0 && dy == 0 && dz == 1));

            }).ToList();

            cubes[a].SidesExposed -= adjacent.Count;
        }
        var sum = cubes.Values.Select(x => x.SidesExposed).Sum();
        Console.WriteLine(sum);
        return sum;
    }
}