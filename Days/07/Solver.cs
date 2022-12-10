namespace Aoc2022.Days._07;

public class Solver
{
    public static async Task Solve()
    {
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "7", "input.txt"));
        var fs = new FileSystem();
        var current = fs.Root;
        foreach (var line in lines)
        {
            current = fs.Apply(current, line);
        }
        SolveA(fs);
        SolveB(fs);
    }

    private static void SolveA(FileSystem fs)
    {
        var sizes = new List<int>();
        var dirs = new List<Node>();
        fs.GetDirectories(fs.Root, dirs);
        foreach (var dir in dirs)
        {
            sizes.Add(dir.GetTotalSize());
        }
        var sum  = sizes.Where(s => s <= 100000).Sum();
        Console.WriteLine(sum);
    }
    private static void SolveB(FileSystem fs)
    {
        var totSize = fs.Root.GetTotalSize();
        var spaceFree = 70000000 - totSize;
        var spaceRequired = 30000000;
        var spaceToFree = spaceRequired - spaceFree;
        var dirs = new List<Node>();
        fs.GetDirectories(fs.Root, dirs);
        var sizes = new List<int>();
        foreach (var dir in dirs)
        {
            sizes.Add(dir.GetTotalSize());
        }

        var candidate = sizes.Where(x => x >= spaceToFree).Min();
        Console.WriteLine(candidate);
    }
}


