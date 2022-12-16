using Jace;

namespace Aoc2022.Days._11;

public class Solver
{
    private static readonly CalculationEngine CalcEngine = new();

    public static async Task Solve()
    {
        var lines = await File.ReadAllTextAsync(Path.Combine("Days", "11", "sample.txt"));
        var monkeys = lines.Split($"{Environment.NewLine}{Environment.NewLine}")
            .Select(ParseMonkey)
            .ToList();

        var worryEngineA = new WorryEngine(monkeys, 20);
        SolveA(monkeys, worryEngineA);

        monkeys = lines.Split($"{Environment.NewLine}{Environment.NewLine}")
            .Select(ParseMonkey)
            .ToList();
        var worryEngineB = new WorryEngine(monkeys, 10000);
        SolveB(monkeys, worryEngineB);
    }

    private static void SolveB(List<Monkey> monkeys, WorryEngine engine)
    {
        var stops = new int[] { 1, 20, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 };
        for (var i = 0; i < engine.Rounds; i++)
        {
            engine.RoundB();
            if (stops.Contains(i+1))
            {
                Console.WriteLine($"== After round {i+1} ==");
                foreach (var m in monkeys)
                {
                    Console.WriteLine($"Monkey {m.Id} inspected items {m.Inspections} times.");
               
                }
                Console.ReadLine();
            }

        }
        var inspections = monkeys.OrderByDescending(x => x.Inspections).Take(2).Select(x => x.Inspections).ToList();
        var total = (long)inspections[0] * (long)inspections[1];
        Console.WriteLine(total);
    }

    private static void SolveA(List<Monkey> monkeys, WorryEngine engine)
    {
        for (var i = 0; i < engine.Rounds; i++)
        {
            engine.RoundA();
        }

        var inspections = monkeys.OrderByDescending(x => x.Inspections).Take(2).Select(x=>x.Inspections).ToList();
        var total = inspections[0] * inspections[1];
        Console.WriteLine(total);
    }

    private static Monkey ParseMonkey(string block)
    {
        var lines = block.Split(Environment.NewLine);
        var id = Convert.ToInt32(lines[0].Split(' ').Skip(1).Last()[..^1]);
        var formulaText = lines[2].Replace("Operation: new = ", string.Empty).Trim();
        var parts = formulaText.Split('*', '+').Select(x => x.Trim()).ToList();
        Func<long,long> fn = input =>
        {

            if (parts[1] == "old")
            {
                return input * input;
            }

            if (formulaText.Contains("+"))
            {
                return input + long.Parse(parts[1]);
            }

            return input * long.Parse(parts[1]);
        };

        var items = lines[1]
            .Replace("Starting items: ", string.Empty)
            .Split(", ")
            .Select(long.Parse)
            .ToList();
        var monkey = new Monkey(id, long.Parse(lines[3].Split(' ').Last()))
        {
            Items = items,
            ItemObjs = items.Select(x=>new Item(x)).ToList(),
            Operation = fn,
            IsMult = formulaText.Contains("*"),
            IsSquare = formulaText == "old * old",
            Operand = formulaText.Split('*','+').Select(x=>x.Trim()).Last(),
            IfTrue = Convert.ToInt32(lines[4].Split(' ').Last()),
            IfFalse = Convert.ToInt32(lines[5].Split(' ').Last())
        };
        return monkey;
    }
}