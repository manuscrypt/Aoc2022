namespace Aoc2022.Days._19;

public class Node : IEquatable<Node>
{
    public bool Visited { get; set; }
    public ResourceDict Resources { get; set; }
    public ResourceDict Robots { get; set; }
    public int TimeRemaining { get; set; }

    public Node(ResourceDict resources, ResourceDict robots, int timeRemaining)
    {
        Resources = resources;
        Robots = robots;
        TimeRemaining = timeRemaining;
    }

    public List<Node> GetNeighbors(Blueprint blueprint)
    {
        if (TimeRemaining == 0)
        {
            return new List<Node>();
        }
        if (TimeRemaining == 1)
        {
            var newResources = new ResourceDict(Resources);
            var newRobots = new ResourceDict(Robots);
            foreach (var robot in Robots)
            {
                newResources[robot.Key] += robot.Value;
            }
            return new List<Node>{ new Node(newResources, newRobots, 0) };
        }
        var neighbors = new List<Node>();

        var noBuyResources = new ResourceDict(Resources);
        foreach (var robot in Robots)
        {
            noBuyResources[robot.Key] += robot.Value;
        }
        neighbors.Add(new Node(noBuyResources, new ResourceDict(Robots), TimeRemaining - 1));

        foreach (var rt in blueprint.RobotTypes)
        {
            if (CanAfford(rt, Resources))
            {
                var newNode = BuyAndCollect(rt);
                neighbors.Add(newNode);
            }
        }
        
        return neighbors;
    }

    private Node BuyAndCollect(RobotType rt)
    {
        var newResources = new ResourceDict(Resources);
        newResources[rt.Cost1.Resource] -= rt.Cost1.Amount;
        if (rt.Cost2 != null)
        {
            newResources[rt.Cost2.Resource] -= rt.Cost2.Amount;
        }

        var newRobots = new ResourceDict(Robots);
        newRobots[rt.Kind] += 1;
        foreach (var robot in Robots)
        {
            newResources[robot.Key] += robot.Value;
        }
        return new Node(newResources, newRobots, TimeRemaining - 1);
    }

    public bool CanAfford(RobotType rt, ResourceDict resources)
    {
        return (rt.Cost1.Amount <= resources[rt.Cost1.Resource] && rt.Cost2 != null && rt.Cost2.Amount <= resources[rt.Cost2.Resource]) ||
               (rt.Cost1.Amount <= resources[rt.Cost1.Resource] && rt.Cost2 == null);
    }

    public bool Equals(Node? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return DictEquals(Resources,other.Resources) && DictEquals(Robots,other.Robots) && TimeRemaining == other.TimeRemaining;
    }

    private bool DictEquals(ResourceDict a, ResourceDict b)
    {
        return a.Count == b.Count && !a.Except(b).Any();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Node)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Resources, Robots, TimeRemaining);
    }
}