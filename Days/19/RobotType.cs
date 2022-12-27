namespace Aoc2022.Days._19;

public sealed record Cost(ResourceType Resource, int Amount);

public class RobotType
{
    public string? KindName => Enum.GetName(typeof(ResourceType), Kind);
    public ResourceType Kind { get; }
    public Cost Cost1 { get; }
    public Cost? Cost2 { get; }

    public RobotType(ResourceType kind, Cost cost1, Cost? cost2)
    {
        Kind = kind;
        Cost1 = cost1;
        Cost2 = cost2;
    }
}