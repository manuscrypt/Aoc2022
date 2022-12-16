namespace Aoc2022.Days._12;

public class Solver
{
    public static async Task Solve()
    {
        var grid = ParseInput(await File.ReadAllLinesAsync(Path.Combine("Days", "12", "input.txt")));
        SolveA(grid);
        SolveB(grid);
    }

    public static void SolveA(char[,] grid)
    {
        var start = Find(grid, 'S').Single();
        var dest = Find(grid, 'E').Single();

        var steps = FindShortestPath(grid,start, dest);

        Console.WriteLine(steps);
    }

    private static void SolveB(char[,] grid)
    {
        var starts = Find(grid, 'S').Concat(Find(grid, 'a')).ToArray();
        var dest = Find(grid, 'E').Single();

        var steps = starts
            .Select(s => FindShortestPath(grid, s, dest))
            .Where(s => s > 0)
            .Min();
        
        Console.WriteLine(steps);
    }

    private static IEnumerable<Point> Find(char[,] grid, char character)
    {
        var points = new List<Point>();
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == character)
                {
                    points.Add(new Point(i, j));
                }
            }
        }

        if (points.Count == 0)
        {
            throw new Exception($"Position for '{character}' not found in grid");
        }

        return points.ToArray();
    }

    private static int FindShortestPath(char[,] map, Point start, Point end)
    {
        var queue = new Queue<Point>();
        queue.Enqueue(start);

        var visited = new bool[map.GetLength(0), map.GetLength(1)];
        var steps = new int[map.GetLength(0), map.GetLength(1)];

        steps[start.X, start.Y] = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == end)
            {
                return steps[current.X, current.Y];
            }

            var neighbors = GetNeighbors(map, current);

            foreach (var neighbor in neighbors)
            {
                if (visited[neighbor.X, neighbor.Y])
                {
                    continue;
                }
                visited[neighbor.X, neighbor.Y] = true;
                steps[neighbor.X, neighbor.Y] = steps[current.X, current.Y] + 1;
                queue.Enqueue(neighbor);
            }
        }

        return -1;
    }
    private static IEnumerable<Point> GetNeighbors(char[,] map, Point point)
    {
        var neighbors = new List<Point>();

        int[] dx = { -1, 0, 1, 0 };
        int[] dy = { 0, 1, 0, -1 };

        for (var i = 0; i < dx.Length; i++)
        {
            var nx = point.X + dx[i];
            var ny = point.Y + dy[i];
            if (nx < 0 || nx >= map.GetLength(0) || ny < 0 || ny >= map.GetLength(1))
            {
                continue;
            }

            var neighbor = new Point(nx, ny);
            if (IsValidNeighbor(map, point, neighbor))
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors.ToArray();
    }
    private static bool IsValidNeighbor(char[,] map, Point current, Point neighbor)
    {
        var currentElevation = map[current.X, current.Y];
        var neighborElevation = map[neighbor.X, neighbor.Y];

        if (currentElevation is 'S')
        {
            currentElevation = 'a';
        }
        if (neighborElevation is 'E')
        {
            neighborElevation = 'z';
        }
        return neighborElevation - currentElevation <= 1;
    }


    private static char[,] ParseInput(string[] lines)
    {
        var grid = new char[lines.Length, lines[0].Length];
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }
        return grid;
    }

}