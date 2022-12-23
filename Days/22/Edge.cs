namespace Aoc2022.Days._22;

internal class Edge
{
    public Face Source { get; }
    public Face Destination { get; }

    public Edge(Face source, Face destination)
    {
        Source = source;
        Destination = destination;
    }
}