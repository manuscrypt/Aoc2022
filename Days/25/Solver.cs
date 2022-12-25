namespace Aoc2022.Days._25;
public class Solver
{
    public static async Task Solve()
    {
        //Console.WriteLine(Snafu("2-10==12-122-=1-1-22"));
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "25", "input.txt"));
        SolveA(lines);
    }

    private static void SolveA(string[] lines)
    {
        var sum = lines.Select(Snafu).Sum();
        Console.WriteLine(sum);
        Console.WriteLine("====================");
        var res = Desnafu(sum);
        Console.WriteLine(res);
        var check = Snafu(res);
        Console.WriteLine(check);
    }

    private static double Snafu(string code)
    {
        var digits = code.ToList();
        digits.Reverse();
        long b = 0;
        double sum = 0;
        foreach (var t in digits)
        {
            sum += DigitToNr(t) * Math.Pow(5,b);
            b+=1;
        }

        return sum;
    }

    private static long DigitToNr(char c)
    {
        return c switch
        {
            '=' => -2,
            '-' => -1,
            '0' => 0,
            '1' => 1,
            '2' => 2,
            _ => throw new ArgumentException(nameof(c))
        };
    }

    private static string Desnafu(double sum)
    {
        var thingies = new Dictionary<long, long>();
        PreDesnafu(thingies, sum);
        var max = thingies.Max(t => t.Key);
        var result = "";
        while (max >= 0)
        {
            var max1 = max;
            if (!thingies.TryGetValue(max1, out var nr))
                nr = 0;
            var digit = NrToDigit(nr);
            result += digit;
            max--;
        }

        return result;
    }
    private static void PreDesnafu(Dictionary<long,long> thingies, double sum)
    {
        if (sum is >= -2 and <= 2)
        {
            thingies.Add(0, (long)sum);
            return;
        }
        double tmp = sum;
        long stellen = 0;
        while (Math.Abs(Math.Round(tmp)) > 2)
        {
            tmp /= 5;
            stellen++;
        }

        var pow = Math.Round(tmp);
        double ded = sum - pow * Math.Pow(5, stellen);
        thingies.Add(stellen, (long)pow);
        if (Math.Abs(ded) > 0)
        {
            PreDesnafu(thingies, ded);
        }
    }

    private static string NrToDigit(long nr)
    {
        return nr switch
        {
            -2 => "=",
            -1 => "-",
            0 => "0",
            1 => "1",
            2 => "2",
            _ => throw new ArgumentException(nameof(nr))
        };
    }
}


