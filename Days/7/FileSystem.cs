namespace Aoc2022.Days._7;

public class FileSystem
{
    public Node Root { get; set; } = new("/");
    public Node Current { get; set; }

    public FileSystem()
    {
        Current = Root;
    }

    public void AddDirectory(string path)
    {
        AddNode(path, 0);
    }
    public void AddFile(string name, int size)
    {
        AddNode(name, size);
    }

    public void GetDirectories(Node current, List<Node> dirs)
    {
        if (current.Children.Any())
        {
            dirs.Add(current);
            foreach (var n in current.Children)
            {
                GetDirectories(n, dirs);
            }
        }
    }
    private void AddNode(string path, int size)
    {
        Current.Children.Add(new Node(path, size) { Parent = Current });
    }

    public void GetAllSizes(Node parent, List<int> sizes)
    {
        sizes.Add(parent.GetTotalSize());
        foreach (var node in parent.Children)
        {
            GetAllSizes(node, sizes);
        }
    }

    public void Apply(string line)
    {
        var parts = line.Split(' ');
        if (parts[0] == "$")
        {
            switch (parts[1])
            {
                case "cd":
                    ChangeDirectory(parts[2]); break;
                case "ls":
                    break;
            }
        }
        else
        {
            switch (parts[0])
            {
                case "dir":
                    AddDirectory(parts[1]);
                    break;
                default:
                    AddFile( parts[1], Convert.ToInt32(parts[0]));
                    break;
            }
        }
    }


    private void ChangeDirectory(string path)
    {
        Current = path switch
        {
            "/" => Root,
            ".." => Current.Parent ?? throw new InvalidOperationException(),
            _ => Current.Children.Single(x => x.Path == path)
        };
    }

    
}