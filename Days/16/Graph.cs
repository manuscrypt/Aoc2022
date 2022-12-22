//class Comp : IComparer<Tuple<Node, int, int>>
//{
//    public int Compare(Tuple<Node, int, int> x, Tuple<Node, int, int> y)
//    {
//        int cmp = y.Item2.CompareTo(x.Item2);
//        if (cmp == 0)
//        {
//            cmp = x.Item3.CompareTo(y.Item3);
//        }

//        return cmp;
//    }
//}

using System.Collections;

public class Graph
{
    public readonly List<Node> Nodes = new List<Node>();
    public readonly List<Edge> Edges  = new List<Edge>();

    public void AddNode(Node node)
    {
        Nodes.Add(node);
    }

    public void AddEdge(Edge edge)
    {
        Edges.Add(edge);
    }

}

public class Node
{
    public int Id { get; }
    public string Name { get; }
    public int Value { get; set; }
    public readonly List<Edge> Outgoing = new List<Edge>();

    public bool IsOpen { get; set; }
    // Additional properties for A* search
    public void AddEdge(Edge edge)
    {
        Outgoing.Add(edge);
    }

    public Node(int id, string name, int value)
    {
        Id = id;
        Name = name;
        Value = value;
    }

    public override string ToString() => Name;

    public IEnumerable<Node> GetNeighbors() => Outgoing.Select(x => x.Destination);
}

public class Edge
{
    public Node Source { get; }
    public Node Destination { get; }

    public Edge(Node source, Node destination)
    {
        Source = source;
        Destination = destination;
    }
}