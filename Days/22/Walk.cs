namespace Aoc2022.Days._22;

internal class Walk: Move
{
    public int Amt { get; }

    public Walk(int amt)
    {
        Amt = amt;
    }
}