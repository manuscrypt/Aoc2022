using System.Text;

namespace Aoc2022.Days._16;

public class Solver
{
    private static readonly Random Rnd = new((int)(DateTime.Now.Ticks));

    public static async Task Solve()
    {
        var input = "sample";
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "16", $"{input}.txt"));
        var graph = CreateGraph(ParseInput(lines));
        //SolveA(graph);
        SolveB(graph);

    }
    public static void SolveA(Graph graph)
    {
        var runner = new MyTraversal2();
        var flowRates = runner.RunA(graph);
        Console.WriteLine(flowRates.Max());
    }
    public static void SolveB(Graph graph)
    {
        var runner = new MyTraversal2();
        var flowRates = runner.RunB(graph);
        Console.WriteLine(flowRates.Max());
        //Console.WriteLine(flowRates.OrderByDescending(x => x).Take(2).ToList().Sum());
    }
    private static List<Valve> ParseInput(string[] lines)
    {
        return lines.Select(Valve.Parse).ToList();
    }

    private static Graph CreateGraph(List<Valve> valves)
    {
        var graph = new Graph();
        int i = 0;
        foreach (var valve in valves)
        {
            graph.AddNode(new Node(i, valve.Name, valve.FlowRate));
            i++;
        }
        foreach (var valve in valves)
        {
            var source = graph.Nodes.Single(n => n.Name == valve.Name);
            foreach (var dest in valve.Connections.OrderBy(x => x)
                         .Select(connection => graph.Nodes.Single(n => n.Name == connection)))
            {
                var edge = new Edge(source, dest);
                graph.AddEdge(edge);
                source.AddEdge(edge);
            }
        }

        return graph;
    }

    private static void CreateMermaid(Graph graph, string filenamePrefix)
    {
        var sb = new StringBuilder();
        sb.AppendLine("```mermaid");
        sb.AppendLine("graph TD;");
        foreach (var node in graph.Nodes)
        {
            foreach (var child in node.GetNeighbors())
            {
                sb.AppendLine($"{node.Name} --> {child.Name}");
            }
        }
        sb.AppendLine("```");
        File.WriteAllText($"{filenamePrefix}.md", sb.ToString());
    }

}
//var str = new StringBuilder();
//str.AppendFormat("\t\t{0}", string.Join("\t\t", relevantNodes.Select(x => $"{x.Name}/{x.Value}")));
//str.AppendLine();


//foreach (var node in relevantNodes)
//{
//    var others = new List<int>();
//    foreach (var other in relevantNodes)
//    {
//        var it = distances.SingleOrDefault(x => x.Item1 == other);
//        if (it != null)
//        {
//            others.Add(it.Item2);
//        }
//        else
//        {
//            others.Add(-1);
//        }
//    }
//    str.AppendFormat("{0}\t\t{1}",node.Name, string.Join("\t\t", others));
//    str.AppendLine();
//}

//Console.WriteLine(str.ToString());

/*
 *     public static bool Dfs(Graph graph, Node node, HashSet<Node> visited)
    {
        // Mark the node as visited
        visited.Add(node);

        // Get the neighbors of the node
        List<Node> neighbors = node.GetNeighbors().ToList();
        if (neighbors.Any())
        {
            // For each neighbor, if it has not been visited, recursively call DFS on it
            // If DFS returns true (indicating a cycle was detected), return true
            foreach (var neighbor in neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    if (Dfs(graph, neighbor, visited))
                    {
                        return true;
                    }
                }
                // If the neighbor has already been visited, a cycle was detected, so return true
                else
                {
                    return true;
                }
            }
        }

        // If no cycles were detected, return false
        return false;
    }*/


//private static void DebugDistances(Graph graph)
//{
//    var aa = graph.Nodes.Single(x => x.Name == "AA");
//    var relevantNodes = graph.Nodes.Where(x => x.Value > 0).ToList();
//    var distances = ShortestDistances.GetDistances(graph, aa, relevantNodes).ToList();
//    foreach (var d in distances)
//    {
//        Console.WriteLine($"AA -> {d.Item1.Name}/{d.Item1.Value} cost {d.Item2}");
//    }

//}

//private static void SolveA(Graph graph)
//{
//    var first = graph.Nodes.Single(x=>x.Name == "AA");
//    int flowRate = 0;
//    int timeRemaining = 30;
//    while (timeRemaining > 0)
//    {
//        var dists = ShortestDistances.GetDistances(graph, first, graph.Nodes.ToList()).ToList();

//        foreach (var d in dists.OrderByDescending(x => x.Item1.Value).ThenBy(x => x.Item2))
//        {
//            Console.WriteLine("{0} -> {1} ({2}) = {3} --> {4}", first, d.Item1.Name, d.Item1.Value, d.Item2,
//                (timeRemaining - (2 * d.Item2 + 1)) * d.Item1.Value);
//        }

//        var winnerPath = dists.MaxBy(d => (timeRemaining - (d.Item2 + 1)) * d.Item1.Value);
//        var winner = winnerPath.Item1;
//        timeRemaining -= winnerPath.Item2;
//        Console.WriteLine($"Winner: {winner.Name} with {winner.Value} at {timeRemaining}");
//        if (winner.Value > 0)
//        {
//            Console.WriteLine($"Opening {winner.Name} at {timeRemaining}");
//            timeRemaining -= 1;
//            Console.WriteLine($"Opened {winner.Name} at {timeRemaining}");
//            flowRate += timeRemaining * winner.Value;
//            winner.Value = 0;
//        }

//        first = winner;
//        Console.WriteLine("FlowRate=" + flowRate);
//    }
//}



//CreateMermaid(graph, input);
//SolveA(graph);

//var aa = graph.Nodes.Single(x => x.Name == "AA");
//var visited = new HashSet<Node>();
//List<Node>? bestPath = null;
//var path = new List<Node>();
//GeneratePaths(graph, aa, 30, path, ref bestPath, relevantNodes);

//foreach (var node in graph.Nodes)
//{
//    Console.WriteLine("======= NODE {0} ========", node.Name);
//    var visited = new HashSet<Node>();
//    var paths = new List<List<Node>>();
//    var path = new List<Node>();
//    GeneratePaths(graph, node, 30, path, paths, relevantNodes);
//    //GeneratePathsWithCycles(graph, node, 30, path, paths, visited);
//    foreach (var p in paths)
//    {
//        Console.WriteLine("{0} ===> {1}", string.Join(" => ", p.Select(x => $"{x.Name}/{x.Value}")),
//            p.Select(x => x.Value).Sum());
//    }
//}
