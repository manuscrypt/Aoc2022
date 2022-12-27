namespace Aoc2022.Days._21;


public class Solver
{
    public static long _resultB = 0;
    private static readonly Dictionary<string, long> Values = new();
    public static async Task Solve()
    {
        var input = "input";
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "21", $"{input}.txt"));
        var nodes = lines.Select(ParseNode).ToList();

        foreach (var n in nodes.Where(x => string.IsNullOrEmpty(x.Operator) && !x.Name.Equals("humn")))
        {
            Values.Add(n.Name, n.Value);
        }

        _resultB = 3509810003705; // nodes.First(x => x.Name.Equals("humn")).Value;

        //SolveA(nodes);
        SolveB(nodes);
    }

    private static void SolveA(List<Node> nodes)
    {
        var root = nodes.Single(x => x.Name == "root");
        var result = Eval(root, nodes);
        Console.WriteLine($"Root yells: {result}");
    }
    private static void SolveB(List<Node> nodes)
    {
        var root = nodes.Single(x => x.Name == "root");
        var left = nodes.Single(x => x.Name == root.Left);
        var right = nodes.Single(x => x.Name == root.Right);
        var containsHuman = false;
        long resultRight = EvalB(right, nodes, ref containsHuman);
        Debug.Assert(!containsHuman);
        Console.WriteLine("Right: " + resultRight);
        long resultLeft = EvalB(left, nodes, ref containsHuman);
        Debug.Assert(containsHuman);
        Console.WriteLine("Left: " + resultLeft);
        while (resultLeft != resultRight)
        {
            long diff = resultLeft - resultRight;
            if (diff > 0)
            {
                _resultB += 500;
            }
            else
            {
                _resultB -= 10;
            }

            resultLeft = EvalB(left, nodes, ref containsHuman);
            Console.WriteLine("Right: " + resultRight);
            Console.WriteLine("Left: " + resultLeft);
        }
        Console.WriteLine(_resultB);
    }
  
    private static long EvalB(Node node, List<Node> nodes, ref bool containsHuman)
    {
        if (node.Name.Equals("humn"))
        {
            containsHuman |= true;
            return _resultB;
        }
        if (Values.ContainsKey(node.Name))
        {
            return Values[node.Name];
        }
        if (!string.IsNullOrEmpty(node.Operator))
        {

            var left = nodes.Single(x => x.Name == node.Left);
            var right = nodes.Single(x => x.Name == node.Right);

            var leftValue = EvalB(left, nodes, ref containsHuman);
            var rightValue = EvalB(right, nodes, ref containsHuman);

            long value = 0;
            switch (node.Operator)
            {
                case "+":
                    value = leftValue + rightValue;
                    break;
                case "*":
                    value = leftValue * rightValue;
                    break;
                case "-":
                    value = leftValue - rightValue;
                    break;
                case "/":
                    value = leftValue / rightValue;
                    break;
                default:
                    throw new Exception("Unknown operator");
            }

            if (!containsHuman)
            {
                Values.Add(node.Name, value);
            }

            return value;

        }

        return node.Value;
    }


    private static long Eval(Node node, List<Node> nodes)
    {
        if (!string.IsNullOrEmpty(node.Operator))
        {
            var left = nodes.Single(x => x.Name == node.Left);
            var right = nodes.Single(x => x.Name == node.Right);
            var leftValue = Eval(left, nodes);
            var rightValue = Eval(right, nodes);

            switch (node.Operator)
            {
                case "+":
                    return leftValue + rightValue;
                case "*":
                    return leftValue * rightValue;
                case "-":
                    return leftValue - rightValue;
                case "/":
                    return leftValue / rightValue;
                default:
                    throw new Exception("Unknown operator");
            }
        }

        return node.Value;
    }
    private static Node ParseNode(string s)
    {
        var parts = s.Split(": ");
        var name = parts[0];
        var node = new Node(name);
        var opParts = parts[1].Split(' ');
        if (opParts.Length == 1)
        {
            node.Value = long.Parse(opParts[0]);
        }
        else
        {
            node.Left = opParts[0];
            node.Operator = opParts[1];
            node.Right = opParts[2];
        }

        return node;
    }
}

[DebuggerDisplay("{Name} = {Value} or {Left} {Operator} {Right}")]
public class Node
{
    public Node(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public long Value { get; set; }
    public string Operator { get; set; }
    public string Left { get; set; }
    public string Right { get; set; }
}