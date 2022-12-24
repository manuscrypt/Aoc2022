namespace Aoc2022.Days._18;

public class Solver
{
    public static async Task Solve()
    {
        var input = "input";
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "18", $"{input}.txt"));
        var points = ParsePoints(lines).ToList();
        var cubes = new List<Cube>();
        foreach (var p in points)
        {
            var cube = new Cube(p);
            cubes.Add(cube);
        }
        SolveA(cubes);
        cubes = new List<Cube>();
        foreach (var p in points)
        {
            var cube = new Cube(p);
            cubes.Add(cube);
        }
        SolveB(cubes);
    }

    private static void SolveB(List<Cube> cubes)
    {
        var exposedSides = SolveA(cubes);
        var xMin = cubes.Min(x => x.Pos.X);
        var yMin = cubes.Min(x => x.Pos.Y);
        var zMin = cubes.Min(x => x.Pos.Z);
        var xMax = cubes.Max(x => x.Pos.X);
        var yMax = cubes.Max(x => x.Pos.Y);
        var zMax = cubes.Max(x => x.Pos.Z);
        for (int x = xMin; x < xMax; x++)
        {
            for (int y = yMin; y < yMax; y++)
            {
                for (int z = zMin; z < zMax; z++)
                {
                    var cube = cubes.SingleOrDefault(c => c.Pos.X == x && c.Pos.Y == y && c.Pos.Z == z);
                    if (cube != null)
                    {
                        continue;
                    }
                    Console.WriteLine($"Air at {x},{y},{z}");
                    var s1 = new Point3d(x - 1, y, z);
                    var s2 = new Point3d(x + 1, y, z);
                    var s3 = new Point3d(x, y - 1, z);
                    var s4 = new Point3d(x, y + 1, z);
                    var s5 = new Point3d(x, y, z-1);
                    var s6 = new Point3d(x, y, z+1);
                    var all = new List<Point3d>
                    {
                        s1, s2, s3, s4, s5, s6
                    };
                    var sideCubes = cubes.Where(c=>all.Contains(c.Pos)).ToList();
                    if (sideCubes.Count > 0)
                    {
                        Console.WriteLine($"{x},{y},{z} has {sideCubes.Count} neighbors");
                        exposedSides -= sideCubes.Count;
                    }
                }
            }
        }
        Console.WriteLine(exposedSides);
    }

    private static IEnumerable<Point3d> ParsePoints(string[] lines)
    {
        return lines.Select(l =>
            new Point3d(l.Split(",").Select(x => Convert.ToInt32(x)).ToArray()));
    }

    private static int SolveA(List<Cube> cubes)
    {
        foreach (var a in cubes)
        {
            var a1 = a;
            var adjacent = cubes.Where(b =>
            {
                var dx = Math.Abs(b.Pos.X - a1.Pos.X);
                var dy = Math.Abs(b.Pos.Y - a1.Pos.Y);
                var dz = Math.Abs(b.Pos.Z - a1.Pos.Z);
                return b != a1 &&
                       ((dx == 1 && dy == 0 && dz == 0) ||
                        (dx == 0 && dy == 1 && dz == 0) ||
                        (dx == 0 && dy == 0 && dz == 1));

            }).ToList();

            a.SidesExposed -= adjacent.Count;
        }
        var sum = cubes.Sum(x => x.SidesExposed);
        Console.WriteLine(sum);
        return sum;
    }
}

internal class Cube
{
    public Point3d Pos { get; }
    public int SidesExposed { get; set; } = 6;
    public Cube(Point3d pos)
    {
        Pos = pos;
    }
}

[DebuggerDisplay("x={X},y={Y},z={Z}")]
internal class Point3d : IEquatable<Point3d>
{
    public Point3d(int[] coords)
    {
        X = coords[0];
        Y = coords[1];
        Z = coords[2];
    }

    public Point3d(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public bool Equals(Point3d? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Point3d)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }
}