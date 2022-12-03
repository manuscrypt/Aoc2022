namespace Aoc2022.Days._2;

public class Solver
{
    public static async Task Solve()
    {
        var input = await File.ReadAllLinesAsync(Path.Combine("Days", "2", "input.txt"));
        SolveA(input);
        SolveB(input);
    }
    public static void SolveA(string[] strings)
    {
        Console.WriteLine(GetPlays(strings).Sum(x => x.GetScore()));
    }

    public static void SolveB(string[] strings)
    {
        Console.WriteLine(GetPlaysB(strings).Sum(x => x.GetScore()));
    }
    private static IEnumerable<Play> GetPlays(IEnumerable<string> input)
    {
        return input
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(parts => new Play(ToRps(parts[0]), ToRps(parts[1])));
    }
    private static IEnumerable<Play> GetPlaysB(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var them = ToRps(parts[0]);
            var us = parts[1] switch
            {
                "X" => them.GetLoser(),
                "Y" => them.GetDraw(),
                "Z" => them.GetWinner(),
                _ => throw new ArgumentException()
            };
            yield return new Play(them, us);
        }
    }

    private static Rps ToRps(string s)
    {
        return s switch
        {
            "X" => Rps.Rock,
            "A" => Rps.Rock,
            "Y" => Rps.Paper,
            "B" => Rps.Paper,
            "Z" => Rps.Scissors,
            "C" => Rps.Scissors,
            _ => throw new ArgumentException()
        };
    }
}

internal class Rps
{
    public string Name { get; set; }
    public int Value { get; set; }
    public static Rps Rock = new("Rock", 1);
    public static Rps Paper = new("Paper", 2);
    public static Rps Scissors = new("Scissors", 3);

    private Rps(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public Rps GetWinner()
    {
        if (this == Rock) return Paper;
        if (this == Paper) return Scissors;
        if (this == Scissors) return Rock;
        throw new ArgumentException();
    }
    public Rps GetLoser()
    {
        if (this == Rock) return Scissors;
        if (this == Paper) return Rock;
        if (this == Scissors) return Paper;
        throw new ArgumentException();
    }

    public Rps GetDraw() => this;

    public int Score(Rps other)
    {
        if (this == Rock)
        {
            return other == Rock ? 3 : other == Scissors ? 6 : 0;
        }
        if (this == Paper)
        {
            return other == Paper ? 3 : other == Rock ? 6 : 
                0;
        }
        if (this == Scissors)
        {
            return other == Scissors ? 3 : other == Paper ? 6 : 0;
        }

        throw new InvalidOperationException();
    }
}
internal class Play
{
    public Rps Them { get; }
    public Rps Us { get; }

    public Play(Rps them, Rps us)
    {
        Them = them;
        Us = us;
    }

    public int GetScore()
    {
        return Us.Score(Them) + Us.Value;
    }
}
