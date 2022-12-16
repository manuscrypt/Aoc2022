using System.Numerics;

namespace Aoc2022.Days._11;

public class Item
{
    public Item(long value)
    {
        Value = value;
    }

    public long Value { get; set; }
    public List<long> Factors { get; set; } = new();
    public List<long> Offsets { get; set; } = new();

    public void AddFactor(long toInt64)
    {
        if (Factors.All(f => f != toInt64))
        {
            Factors.Add(toInt64);
        }
    }
}

public class Monkey
{
    public Monkey(int id, long divisibleBy)
    {
        Id = id;
        DivisibleBy = divisibleBy;
    }

    public int Id { get; set; }
    public List<long> Items { get; set; } = new();
    public List<Item> ItemObjs { get; set; } = new();
    public Func<long, long> Operation { get; set; }
    public int Inspections { get; set; } = 0;
    public long DivisibleBy { get; set; }
    public int IfTrue { get; set; }
    public int IfFalse { get; set; }
    public string Operand { get; set; }
    public bool IsMult { get; set; }
    public bool IsSquare { get; set; }

    public bool IsDivisibleBy(long worryLevel)
    {
        return worryLevel % DivisibleBy == 0;
    }
    public int TestFunc(long worryLevel)
    {
        return IsDivisibleBy(worryLevel) ? IfTrue : IfFalse;
    }

    public void Debug()
    {
        Console.WriteLine($"Monkey {Id}:");
        Console.WriteLine($"\tInspections {Inspections}:");
        Console.WriteLine($"\tStarting items: {string.Join(", ", Items)}");
        Console.WriteLine($"\tTest: divisible by {DivisibleBy}");
        Console.WriteLine($"\t\tIf true: throw to monkey {IfTrue}");
        Console.WriteLine($"\t\tIf false: throw to monkey {IfFalse}");
    }
}