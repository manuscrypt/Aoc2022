namespace Aoc2022.Days._13;

using System.Collections.Generic;

public class ListComparer : IComparer<NestedArray>
{
    public int Compare(NestedArray? x, NestedArray? y)
    {
        return CompareValues(x, y);
    }

    private int CompareValues(object? x, object? y)
    {
        switch (x)
        {
            case null when y == null:
                return 0;
            case null:
                return -1;
        }

        if (y == null) return 1;
        while (true)
        {
            if (x is int a && y is int b)
            {
                return a.CompareTo(b);
            }

            if (x is List<object> la && y is List<object> lb)
            {
                return CompareLists(la, lb);
            }

            if (x is int xi)
            {
                x = new List<object> { xi };
                continue;
            }

            y = new List<object> { y };
        }
    }

    public int CompareLists(List<object> x, List<object> y)
    {
        var max = Math.Max(x.Count, y.Count);
        for (var i = 0; i < max; i++)
        {
            var left = x.Count > i ? x[i] : null;
            var right = y.Count > i ? y[i] : null;
            var r = CompareValues(left, right);
            if (r != 0)
            {
                return r;
            }
        }
        return 0;
    }
}
