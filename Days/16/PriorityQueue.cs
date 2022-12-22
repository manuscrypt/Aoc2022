namespace Aoc2022.Days._16;

public class NodeComparer : IComparer<Node>
{
    public int Compare(Node? x, Node? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        return x.Value.CompareTo(y.Value);
    }
}
public class PriorityQueue
{
    private readonly SortedSet<Node> _set;

    public int Count => _set.Count;
    public PriorityQueue(IComparer<Node> comparer)
    {
        _set = new SortedSet<Node>(comparer);
    }

    public void Enqueue(Node node)
    {
        _set.Add(node);
    }
    public Node Dequeue()
    {
        return _set.First();
    }
}