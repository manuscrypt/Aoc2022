namespace Aoc2022.Days._22;

internal class Route
{
    public List<Move> Moves { get; } = new();
    public void AddMove(Move m)
    {
        Moves.Add(m);
    }
}