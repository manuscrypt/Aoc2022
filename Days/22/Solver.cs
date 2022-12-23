using System.Text;

namespace Aoc2022.Days._22;

public class Solver
{
    public static async Task Solve()
    {
        var text = await File.ReadAllTextAsync(Path.Combine("Days", "22", "sample.txt"));
        var blocks = text.Split($"{Environment.NewLine}{Environment.NewLine}");

        var (chars,faces) = ToFaces(blocks[0], 4);
        var route  = ParseRoute(blocks[1]);
        //SolveA(chars, route);

        var graph = new Graph(faces,4);
        graph.LabelFaces();
        //CreateMermaid(graph, "cube.md");
        //SolveB(graph, chars, route);
        //DebugGrid(grid);
    }

    private static void SolveB(Graph graph, char[][] chars, Route route)
    {
        var face = graph.Faces.OrderBy(x => x.Pos).First(f => f.Pos.Y == 0);
        var cell = face.Cells.First(x => x.C == '.');
        var direction = Direction.Right;
        foreach (var move in route.Moves)
        {
            if (move is Turn t)
            {
                direction = t.Dir switch
                {
                    'L' => direction - 1 < 0 ? Direction.Up : direction - 1,
                    'R' => direction + 1 > Direction.Up ? Direction.Right : direction + 1,
                    _ => throw new ArgumentOutOfRangeException()
                };
                Console.WriteLine($"turned {t.Dir}. Now facing {Enum.GetName(typeof(Direction), direction)}");
            }
            else if (move is Walk w)
            {
                for (int i = 0; i < w.Amt; i++)
                {
                    var (dx, dy) = GetOffset(direction);
                    var (x, y) = Step(cell.Pos.X, cell.Pos.Y, dx, dy, chars.Length, chars[cell.Pos.X].Length);
                    var (newFace, newCell, newDirection) = graph.GetFace(face, direction, x, y);

                    var it = newCell.C;
                    if (it == '#')
                    {
                        break;
                    }
                    if (it == '.')
                    {
                        cell = newCell;
                        direction = newDirection;
                        face = newFace;
                        continue;
                    }

                    while (it == ' ')
                    {
                        (x, y) = Step(x, y, dx, dy, chars.Length, chars[x].Length);
                        it = chars[x][y];
                    }
                }
            }
        }
    }

    private static void SolveA(char[][] chars, Route route)
    {
        var y = 0;
        var x = new string(chars[0]).IndexOf('.');

        var direction = Direction.Right;
        foreach (var move in route.Moves)
        {
            if (move is Turn t)
            {
                direction = t.Dir switch
                {
                    'L' => direction - 1 < 0 ? Direction.Up : direction - 1,
                    'R' => direction + 1 > Direction.Up ? Direction.Right : direction + 1,
                    _ => throw new ArgumentOutOfRangeException()
                };
                Console.WriteLine($"turned {t.Dir}. Now facing {Enum.GetName(typeof(Direction), direction)}");
            }
            else if (move is Walk w)
            {
                for (int i = 0; i < w.Amt; i++)
                {
                    var (dx, dy) = GetOffset(direction);
                    var it = chars[x + dx][y + dy];
                    if (it == '#')
                    {
                        break;
                    }
                    if (it == '.')
                    {
                        (x,y) = Step(x, y, dx, dy, chars.Length, chars[x].Length);
                        continue;
                    }

                    while (it == ' ')
                    {
                        (x,y) = Step(x, y, dx, dy, chars.Length, chars[x].Length);
                        it = chars[x][y];
                    }
                }
                Console.WriteLine($"moved to {x},{y}");

            }
        }

        var result = 1000 * (y+1) + 4 * (x+1) + direction;
        Console.WriteLine(result);
    }

    private static (int x, int y) Step(int x, int y, int dx, int dy, int maxX, int maxY)
    {
        x += dx;
        y += dy;
        if (x == maxX) x = 0;
        if (y == maxY) y = 0;
        if (x < 0) x = maxX - 1;
        if (y < 0) y = maxY - 1;
        return (x, y);
    }
    private static (int, int) GetOffset(Direction dir)
    {
        return dir switch
        {
            Direction.Right => (1, 0),
            Direction.Down => (0, 1),
            Direction.Left => (-1, 0),
            Direction.Up => (0, -1),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static void DebugGrid(char[,] grid)
    {
        var width = grid.GetLength(0);
        var height = grid.GetLength(1);
        var sb = new StringBuilder();
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                sb.Append(grid[i, j]);
            }

            sb.AppendLine();
        }
        Console.WriteLine(sb.ToString());
    }
    private static void CreateMermaid(Graph graph, string filenamePrefix)
    {
        var sb = new StringBuilder();
        sb.AppendLine("```mermaid");
        sb.AppendLine("graph TD;");
        foreach (var node in graph.Faces)
        {
            foreach (var child in graph.GetNeighbors(node))
            {
                sb.AppendLine($"{node.Pos} --> {child.Pos}");
            }
        }
        sb.AppendLine("```");
        File.WriteAllText($"{filenamePrefix}.md", sb.ToString());
    }
    private static (char[][], List<Face>) ToFaces(string block, int cubeSize)
    {
        var faces = new List<Face>();
        var lines = block.Split($"{Environment.NewLine}");
        var max = lines.Max(x => x.Length);
        var items = lines.Select(l =>
        {
            while (l.Length < max)
            {
                l += ' ';
            }
            return l.ToArray();
        }).ToArray();

        var cx = max / cubeSize;
        var cy = lines.Length / cubeSize;

        var chars = new char[cx * cubeSize][];
        for (int i = 0; i < cx*cubeSize; i++)
        {
            chars[i] = new char[cy *cubeSize];
        }

        for (var i = 0; i < cx; i++)
        {
            for (var j = 0; j < cy; j++)
            {
                var face = new Face(new Point(i, j));
                for (var k = 0; k < cubeSize; k++)
                {
                    for (var l = 0; l < cubeSize; l++)
                    {
                        var x = i * cubeSize + k;
                        var y = j * cubeSize + l;
                        var line = items[y];
                        var it = line[x];
                        chars[x][y] = it;
                        if (it != ' ')
                        {
                            face.Cells.Add(new Cell(new Point(k, l), it));
                        }
                    }
                }
                if(face.Cells.Any())
                    faces.Add(face);    
            }
        }
        return (chars,faces);
    }

    private static Route ParseRoute(string block)
    {
        var token = string.Empty;
        var p = new Route();
        foreach (var c in block)
        {
            if (char.IsDigit(c))
            {
                token += c;
            }
            else
            {
                if (!string.IsNullOrEmpty(token))
                {
                    p.AddMove(new Walk(Convert.ToInt32(token)));
                    token = string.Empty;
                }

                p.AddMove(new Turn(c));
            }
        }
        if (!string.IsNullOrEmpty(token))
        {
            p.AddMove(new Walk(Convert.ToInt32(token)));
        }

        return p;
    }

}