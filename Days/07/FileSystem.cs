namespace Aoc2022.Days._07;

public class FileSystem
{
    public Node Root { get; set; } = new("/");

    public void GetDirectories(Node current, List<Node> dirs)
    {
        if (!current.Children.Any()) return;
        dirs.Add(current);
        foreach (var n in current.Children)
        {
            GetDirectories(n, dirs);
        }
    }

    public Node Apply(Node current, string line)
    {
        var parts = line.Split(' ');
        if (parts[0] == "$")
        {
            switch (parts[1])
            {
                case "cd":
                    return ChangeDirectory(current, parts[2]);
            }
        }
        else
        {
            switch (parts[0])
            {
                case "dir":
                    current.AddNode(parts[1]);
                    break;
                default:
                    current.AddNode(parts[1], Convert.ToInt32(parts[0]));
                    break;
            }
        }

        return current;
    }


    private Node ChangeDirectory(Node current, string path)
    {
        return path switch
        {
            "/" => Root,
            ".." => current.Parent ?? throw new InvalidOperationException(),
            _ => current.Children.Single(x => x.Path == path)
        };
    }
}