namespace Aoc2022.Days._13;

public class Solver
{
    private static readonly ListComparer Comparer = new();
    public static async Task Solve()
    {
        var allText = await File.ReadAllTextAsync(Path.Combine("Days", "13", "input.txt"));
        var pairsA = ParseInput(allText).ToList();

        SolveA(pairsA);

        allText += $"{Environment.NewLine}{Environment.NewLine}";
        allText += $"[[2]]{Environment.NewLine}";
        allText += $"[[6]]";
        var pairsB = ParseInput(allText)
            .ToList();
        SolveB(pairsB);
    }

    public static void SolveA(List<Tuple<NestedArray, NestedArray>> pairs)
    {
        var idx = new List<int>();
        for (var i = 0; i < pairs.Count; i++)
        {
            var p = pairs[i];
            if (Comparer.Compare(p.Item1, p.Item2) <= 0)
            {
                idx.Add(i + 1);
            }
        }
        Console.WriteLine(idx.Sum());
    }
    public static void SolveB(List<Tuple<NestedArray, NestedArray>> pairs)
    {
        var allPackets = pairs.Select(p => p.Item1)
            .Concat(pairs.Select(p => p.Item2))
            .ToArray();

        Array.Sort(allPackets, Comparer);

        var lines = allPackets.Select(p => p.AsString).ToList();

        var idx1 = lines.IndexOf("[[2]]");
        var idx2 = lines.IndexOf("[[6]]");

        Console.WriteLine((idx1 + 1) * (idx2 + 1));
    }

    private static IEnumerable<Tuple<NestedArray, NestedArray>> ParseInput(string input)
    {
        var blocks = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        foreach (var block in blocks)
        {
            var lines = block.Split(Environment.NewLine);
            var left = ParseNestedArray(lines[0]);
            var right = ParseNestedArray(lines[1]);
            yield return Tuple.Create(left, right);
        }
    }

    public static NestedArray ParseNestedArray(string input)
    {
        var index = 0;

        NestedArray? current = null;
        var token = "";
        while (index < input.Length)
        {
            var c = input[index];
            if (c == '[')
            {
                if (current == null)
                {
                    current = new NestedArray(null);
                }
                else
                {
                    AddFromToken(token, current, c);
                    var newArr = new NestedArray(current);
                    current.Add(newArr);
                    current = newArr;
                }
            }
            else if (c == ']')
            {
                if (current != null)
                {
                    AddFromToken(token, current, c);
                    token = string.Empty;
                    if (current.Parent != null)
                    {
                        current = current.Parent;
                    }
                }
            }
            else if (c == ',')
            {
                if (current != null)
                {
                    AddFromToken(token, current, c);
                    token = string.Empty;
                }
            }
            else
            {
                token += c;
            }

            index++;
        }

        return current ?? new NestedArray(null);
    }

    private static void AddFromToken(string token, NestedArray current, char c)
    {
        if (!string.IsNullOrEmpty(token))
        {
            if (int.TryParse(token, out int integer))
            {
                current.Add(integer);
            }
            else
            {
                Console.WriteLine("Not an int: " + token);
            }
        }
    }
}