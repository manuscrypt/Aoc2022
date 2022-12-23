namespace Aoc2022.Days._22;

internal class Turn : Move
{
    public char Dir { get; }

    public Turn(char dir)
    {
        Dir = dir;
    }
}