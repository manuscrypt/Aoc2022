namespace Aoc2022.Days._8;

public class Solver
{
    public static async Task Solve()
    {
        var lines = await File.ReadAllLinesAsync(Path.Combine("Days", "8", "sample.txt"));
        var data = Parse(lines);
        SolveA(data);
        SolveB(data);
    }

    private static int[,] Parse(string[] lines)
    {
        var data = new int[lines.Length, lines.Length];
        for (var row = 0; row < lines.Length; row++)
        {
            var line = lines[row];
            var array = line.Select(x => x.ToString()).Select(x => Convert.ToInt32(x)).ToArray();
            for (var col = 0; col < array.Length; col++)
            {
                data[col, row] = array[col];
            }
        }

        return data;
    }

    private static void SolveA(int[,] data)
    {
        var count = 0;
        for (var x = 0; x < data.GetLength(0); x++)
        {
            for (var y = 0; y < data.GetLength(1); y++)
            {
                if (IsVisible(data[x, y], data, x, y))
                {
                    count++;
                }
            }
        }
        Console.WriteLine(count);
    }
    private static void SolveB(int[,] data)
    {
        var max = 0;
        for (var x = 0; x < data.GetLength(0); x++)
        {
            for (var y = 0; y < data.GetLength(1); y++)
            {
                var vd = GetViewingDistance(data[x, y], data, x, y);
                if (vd > max)
                {
                    max = vd;
                }
            }
        }

        Console.WriteLine(max);
    }

    private static bool IsVisible(int tree, int[,] data, int x, int y)
    {
        if (x == 0) return true;
        if (y == 0) return true;
        if (x == data.GetLength(0) - 1) return true;
        if (y == data.GetLength(1) - 1) return true;

        var colVals = GetColumn(data, x);
        var maxUp = GetMax(colVals, 0, y);
        var maxDown = GetMax(colVals, y + 1, data.GetLength(0));
        var visibleUpDown = (maxUp < tree || maxDown < tree);

        var rowVals = GetRow(data, y);
        var maxLeft = GetMax(rowVals, 0, x);
        var maxRight = GetMax(rowVals, x + 1, data.GetLength(1));
        var visibleLeftRight = (maxLeft < tree || maxRight < tree);

        return visibleUpDown || visibleLeftRight;
    }
    private static int GetViewingDistance(int tree, int[,] data, int x, int y)
    {
        var column = GetColumn(data, x);
        var row = GetRow(data, y);

        var vdUp = GetVdLeftUp(tree, column, y);
        var vdLeft = GetVdLeftUp(tree, row, x);
        var vdDown = GetVdRightDown(tree, column, y);
        var vdRight = GetVdRightDown(tree, row, x);
        
        return vdLeft * vdRight * vdUp * vdDown;
    }
    private static T[] GetColumn<T>(T[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
            .Select(x => matrix[columnNumber, x])
            .ToArray();
    }

    private static T[] GetRow<T>(T[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
            .Select(x => matrix[x, rowNumber])
            .ToArray();
    }

    private static int GetMax(int[] arr, int start, int end)
    {
        return arr.Skip(start).Take(end - start).DefaultIfEmpty(0).Max();
    }

    private static int GetVdLeftUp(int tree, int[] arr, int start)
    {
        var cursor = start - 1;
        var viewingDistance = 0;
        while (cursor >= 0)
        {
            var height = arr[cursor];
            if (height < tree)
            {
                viewingDistance++;
                cursor--;
            }
            else
            {
                viewingDistance++;
                break;
            }
        }

        return viewingDistance;
    }
    private static int GetVdRightDown(int tree, int[] arr, int start)
    {
        var cursor = start + 1;
        var viewingDistance = 0;
        while (cursor < arr.Length)
        {
            var height = arr[cursor];
            if (height < tree)
            {
                viewingDistance++;
                cursor++;
            }
            else
            {
                viewingDistance++;
                break;
            }
        }

        return viewingDistance;
    }
}


