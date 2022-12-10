namespace Aoc2022.Days._07;

public class Node
{
    public Node(string path, int size = 0)
    {
        Path = path;
        Size = size;
    }

    public string Path { get; set; }
    public int Size { get; }
    public Node? Parent { get; set; }
    public List<Node> Children { get; set; } = new();

    public void AddNode(string path, int size = 0)
    {
        Children.Add(new Node(path, size) { Parent = this });
    }
    public int GetTotalSize()
    {
        return Size + Children.Sum(node => node.GetTotalSize());
    }
}