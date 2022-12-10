using System.Text.RegularExpressions;

namespace Aoc2022.Days._05;

internal record Move(int Count, int From, int To);

public partial class Solver
{
    [GeneratedRegex("move (?<count>\\d+) from (?<from>\\d+) to (?<to>\\d+)", RegexOptions.Singleline)]
    private static partial Regex MyRegex();

    private static readonly Regex Regex = MyRegex();
    
    public static async Task Solve()
    {
        var lines = await File.ReadAllTextAsync(Path.Combine("Days", "5", "sample.txt"));
        var split = lines.Split($"{Environment.NewLine}{Environment.NewLine}");

        var stacks = ParseStacks(split[0]);
        var moves = ParseMoves(split[1]);

        SolveA(stacks, moves);

        stacks = ParseStacks(split[0]);
        SolveB(stacks, moves);
    }

    private static void SolveA(IReadOnlyList<Stack<char>> stacks, IEnumerable<Move> moves)
    {
        foreach (var move in moves)
        {
            var fromStack = stacks[move.From - 1];
            var toStack = stacks[move.To - 1];

            for (var i = 0; i < move.Count; i++)
            {
                toStack.Push(fromStack.Pop());
            }
        }
        Console.WriteLine(string.Join("", stacks.Select(x => x.Peek())));
    }
    private static void SolveB(IReadOnlyList<Stack<char>> stacks, IEnumerable<Move> moves)
    {
        foreach (var move in moves)
        {
            var fromStack = stacks[move.From - 1];
            var toStack = stacks[move.To - 1];
            var e = fromStack.Take(move.Count).Reverse().ToArray();
            for (var i = 0; i < move.Count; i++)
            {
                fromStack.Pop();
                toStack.Push(e[i]);
            }
        }
        Console.WriteLine(string.Join("", stacks.Select(x => x.Peek())));
    }
    private static IReadOnlyList<Move> ParseMoves(string s)
    {
        var lines = s.Split(Environment.NewLine);

        return (from line in lines
                select Regex.Match(line) into match
                let count = Convert.ToInt32(match.Groups["count"].Value)
                let @from = Convert.ToInt32(match.Groups["from"].Value)
                let to = Convert.ToInt32(match.Groups["to"].Value)
                select new Move(count, @from, to)).ToList();
    }

    private static IReadOnlyList<Stack<char>> ParseStacks(string s)
    {
        var lines = s.Split(Environment.NewLine);
        var count = lines.Last().Split("   ")
            .Select(x => x.Trim())
            .Select(x => Convert.ToInt32(x))
            .Last();

        var stacks = new List<Stack<char>>(count);

        for (var i = 0; i < count; i++)
        {
            var stackN = new Stack<char>();
            stacks.Add(stackN);
            foreach (var line in lines.Reverse().Skip(1))
            {
                var pos = i * 4;
                if (line[pos] == '[')
                {
                    stackN.Push(line[pos + 1]);
                }
            }
        }

        return stacks;
    }


}


