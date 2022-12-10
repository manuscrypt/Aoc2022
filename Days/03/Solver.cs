namespace Aoc2022.Days._03;

public class Solver
{
    public static async Task Solve()
    {
        var input = await File.ReadAllLinesAsync(Path.Combine("Days", "3", "input.txt"));
        SolveA(input);
        SolveB(input);
    }

    public static void SolveA(string[] strings)
    {
        Console.WriteLine(strings.Select(SplitLine)
            .Select(it =>
                MapChar(string.Join("", it.Item1.Intersect(it.Item2))))
            .Sum());
    }
    public static void SolveB(string[] strings)
    {
        Console.WriteLine(
            strings.Chunk(3).
            Select(c => MapChar(string.Join("", c[0].Intersect(c[1]).Intersect(c[2]))))
            .Sum());
    }

    private static (string, string) SplitLine(string input)
    {
        var length = input.Length / 2;
        return (input[..length], input[length..]);
    }

    private static int MapChar(string s)
    {
        if (s.ToLower() == s)
        {
            return s[0] - 96;
        }

        return s[0] - 64 + 26;
    }
}