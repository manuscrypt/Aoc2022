namespace Aoc2022.Days._19;

public class Blueprint
{
    public int Id { get; }
    public List<RobotType> RobotTypes { get; set; } = new();
    public int GeodeCost { get; set; }
    public RobotType GeodeRobotType { get; set; }

    public Blueprint(int id)
    {
        Id = id;
    }

    public void AddRobotType(RobotType robotType)
    {
        RobotTypes.Add(robotType);
        if (robotType.Kind == ResourceType.Geode)
        {
            GeodeRobotType = robotType;
            GeodeCost = robotType.Cost2?.Amount ?? int.MaxValue;
        }
    }

    //private double GetGeodeFactor()
    //{
    //    var r1 = RobotTypes.Single(x => x.Kind.Equals(ResourceKind.Geode));
    //    return r1.Cost2.Amount / r1.Cost1.Amount;
    //}
    //private double GetObsidianFactor()
    //{
    //    var r1 = RobotTypes.Single(x => x.Kind.Equals(ResourceKind.Obsidian));
    //    return r1.Cost2.Amount / r1.Cost1.Amount;
    //}
}