namespace Aoc2022.Days._22;

internal class Route
{
    public List<Move> Moves { get; } = new();
    public void AddMove(Move m)
    {
        Moves.Add(m);
    }
}

public abstract class Move
{

}

internal class Turn : Move
{
    public char Dir { get; }

    public Turn(char dir)
    {
        Dir = dir;
    }
}

internal class Walk : Move
{
    public int Amt { get; }

    public Walk(int amt)
    {
        Amt = amt;
    }
}