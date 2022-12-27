namespace Aoc2022.Days._20;


public class Solver
{
    public static async Task Solve()
    {
        var input = "input";
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "20", $"{input}.txt"));
        var nums = lines.Select((x,i) => (i,long.Parse(x))).ToList();

        //SolveA(nums);
        //SolveB(nums);
    }

    private static void SolveB(List<(int i, long)> nums)
    {
        List<(int i, long)> mult = new List<(int i, long)>();
        foreach (var (idx, num) in nums)
        {
            mult.Add((idx,num * 811589153));
        }

        var rb = new RingBuffer(mult);
        for (int i = 0; i < 10; i++)
        {
            foreach (var (idx, num) in nums.ToList())
            {
                rb.MoveNumWithIdx(idx);
            }
            Console.WriteLine($"After {i+1} rounds of mixing:");
            Console.WriteLine(rb.ToString());
        }
        GetResult(nums, rb);
    }

    private static void SolveA(List<(int idx, long num)> nums)
    {
        var ringBuffer = new RingBuffer(nums);
        //Console.WriteLine(ringBuffer.ToString());
        foreach (var (idx,num) in nums.ToList())
        {
            ringBuffer.MoveNumWithIdx(idx);
            //Console.WriteLine(ringBuffer.ToString());
        }
        GetResult(nums, ringBuffer);
    }

    private static void GetResult(List<(int idx, long num)> nums, RingBuffer ringBuffer)
    {
        var listIndexOf0 = ringBuffer.GetListIndexOfNumWithVal(0);
        var vals = new List<long>();
        for (int i = 1000; i <= 3000; i += 1000)
        {
            var newIdx = (listIndexOf0 + i) % nums.Count;
            vals.Add(ringBuffer.GetElementAtIndex(newIdx));
            listIndexOf0 = ringBuffer.GetListIndexOfNumWithVal(0);
        }

        Console.WriteLine(vals.Sum());
    }
}

internal class RingBuffer
{
    private readonly List<(int idx, long num)> _nums;

    public RingBuffer(List<(int idx, long num)> nums)
    {
        _nums = nums;
    }

    public void MoveNumWithIdx(int idx)
    {
        var num = _nums.Single(x => x.idx == idx);
        if (num.num == 0)
        {
            return;
        }
        var listIndex = _nums.IndexOf(num);
        var newIndex = (listIndex + num.num);
        if (newIndex > _nums.Count)
        {
            newIndex = (newIndex + 1) % _nums.Count;
        }
            
        //Console.WriteLine($"moving {num.num} with idx {num.idx}, currently at {listIndex}, to {newIndex}");
        while (newIndex <= 0)
        {
            newIndex = _nums.Count - 1 + newIndex;
        }

        _nums.RemoveAt(listIndex);
        _nums.Insert((int)newIndex,num);
    }

    public int GetListIndexOfNumWithVal(int i)
    {
        var num = _nums.Single(x => x.num == i);
        return _nums.IndexOf(num);
    }

    public long GetElementAtIndex(int i)
    {
        return _nums.ElementAt(i).num;
    }

    public override string ToString()
    {
        return string.Join(", ", _nums.Select(x => x.num));
    }
}