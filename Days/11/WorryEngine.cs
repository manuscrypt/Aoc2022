using System.Data;

namespace Aoc2022.Days._11;

public class WorryEngine
{
    public int Rounds { get; }
    private readonly List<Monkey> _monkeys;

    public WorryEngine(List<Monkey> monkeys, int rounds)
    {
        Rounds = rounds;
        _monkeys = monkeys;
    }

    public void RoundA()
    {
        _monkeys.ForEach(TurnA);
    }

    public void RoundB()
    {
        foreach (var monkey in _monkeys)
        {
            //monkey.Debug();
            TurnB(monkey);
        }
    }

    public void TurnB(Monkey m)
    {
        var itemsToRemove = new List<Item>();
        var dict = new Dictionary<int, List<Item>>();
        Console.WriteLine($"Turn of monkey {m.Id}");
        foreach(var item in m.ItemObjs)
        {
            Console.WriteLine($"\tBefore: [{string.Join(",", item.Factors)}, value={item.Value}]");
            m.Inspections++;
            item.Value = m.Operation(item.Value);
            var targetMonkey = m.TestFunc(item.Value);
            Console.WriteLine($"\t\tAfter: [{string.Join(",", item.Factors)}, value={item.Value}]");

            if (!dict.TryGetValue(targetMonkey, out var list))
            {
                list = new List<Item>();
                dict.TryAdd(targetMonkey, list);
            }
            dict[targetMonkey].Add(item);
            itemsToRemove.Add(item);
        };

        foreach (var kv in dict)
        {
            _monkeys[kv.Key].ItemObjs.AddRange(kv.Value);
        }

        foreach (var item in itemsToRemove)
        {
            m.ItemObjs.Remove(item);
        }
    }


    public void TurnA(Monkey m)
    {
        var itemsToRemove = new List<long>();
        var dict = new Dictionary<int, List<long>>();
        foreach (var item in m.Items)
        {
            m.Inspections++;
            var worryLevel = m.Operation(item);
            worryLevel /= 3;
            var targetMonkey = m.TestFunc(worryLevel);
            if (!dict.TryGetValue(targetMonkey, out var list))
            {
                list = new List<long>();
                dict.Add(targetMonkey, list);
            }
            dict[targetMonkey].Add(worryLevel);
            itemsToRemove.Add(item);
        }

        foreach (var kv in dict)
        {
            _monkeys[kv.Key].Items.AddRange(kv.Value);
        }

        foreach (var item in itemsToRemove)
        {
            m.Items.Remove(item);
        }
    }

}