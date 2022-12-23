using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Aoc2022.Days._22;

internal class Graph
{
    public List<Face> Faces { get; }
    public int Size { get; }
    public List<Edge> Edges { get; } = new();

    public Graph(List<Face> faces, int size)
    {
        Faces = faces;
        Size = size;
        foreach (var a in Faces)
        {
            foreach (var b in Faces)
            {
                if (a != b)
                {
                    var da = Math.Abs(a.Pos.X - b.Pos.X);
                    var db = Math.Abs(a.Pos.Y - b.Pos.Y);
                    if ((da == 1 && db == 0)|| (da == 0 && db == 1))
                    {
                        Edges.Add(new Edge(a, b));
                    }
                }
            }
        }
    }

    void Dfs(Face face, HashSet<Face> visited)
    {
        visited.Add(face);
        var neighbors = GetNeighbors(face).ToList();
        foreach (var neighbor in neighbors)
        {
            if (!visited.Contains(neighbor))
            {
                FindLabel(face, neighbor);
                Dfs(neighbor, visited);
            }
        }
    }
    public void LabelFaces()
    {
        var cur  = Edges.GroupBy(x => x.Source).OrderByDescending(x => x.Count()).Select(x => x.Key).First();
        cur.Label = Label.Front;
        var visited = new HashSet<Face>();
        Dfs(cur, visited);

        foreach (var face in Faces)
        {
            Console.WriteLine($"Face at pos {face.Pos.X}, {face.Pos.Y} has label {face.Label}");
        }
    }
    public IEnumerable<Face> GetNeighbors(Face f)
    {
        return Edges.Where(x=>x.Source == f).Select(x=>x.Destination);
    }

    private void FindLabel(Face cur, Face neighbor)
    {
        var edgePos = neighbor.Pos - cur.Pos;
        if (cur.Label == Label.Front)
        {
            neighbor.Label = edgePos.X == 0 ? (edgePos.Y > 0 ? Label.Bottom : Label.Top)
                : (edgePos.X > 0 ? Label.Right : Label.Left);
        }
        else if (cur.Label == Label.Top)
        {
            neighbor.Label = edgePos.X == 0 ? (edgePos.Y > 0 ? Label.Back : Label.Front)
                : (edgePos.X > 0 ? Label.Right : Label.Left);
        }
        else if (cur.Label == Label.Back)
        {
            neighbor.Label = edgePos.X == 0 ? (edgePos.Y > 0 ? Label.Bottom : Label.Top)
                : (edgePos.X > 0 ? Label.Left : Label.Right);
        }
        else if (cur.Label == Label.Bottom)
        {
            neighbor.Label = edgePos.X == 0 ? (edgePos.Y > 0 ? Label.Back : Label.Front)
                : (edgePos.X > 0 ? Label.Right : Label.Left);
        }
        else if (cur.Label == Label.Left)
        {
            neighbor.Label = edgePos.X == 0 ? (edgePos.Y > 0 ? Label.Top : Label.Bottom)
                : (edgePos.X > 0 ? Label.Front : Label.Back);
        }
        else if (cur.Label == Label.Right)
        {
            neighbor.Label = edgePos.X == 0 ? (edgePos.Y > 0 ? Label.Top : Label.Bottom)
                : (edgePos.X > 0 ? Label.Back : Label.Front);
        }
    }

    public (Face newFace, Cell newCell, Direction newDirection) GetFace(Face startFace, Direction direction, int x, int y)
    {
        var cell = startFace.Cells.SingleOrDefault(c => c.Pos.X == x && c.Pos.Y == y);
        if (cell != null)
        {
            return (startFace, cell, direction);
        }
        var absX = startFace.Pos.X * Size + x;
        var absY = startFace.Pos.Y * Size + y;
        var newFace = Faces.SingleOrDefault(f => f.Cells.Any(c =>
        {
            var cx = f.Pos.X * Size + c.Pos.X;
            var cy = f.Pos.Y * Size + c.Pos.Y;
            return cx == absX && cy == absY;
        }));
        if (newFace != null)
        {
            if (x >= Size) x = 0;
            if (y >= Size) y = 0;
            if (x < 0) x = Size - 1;
            if (y < 0) y = Size - 1;
            cell = newFace.Cells.Single(c => c.Pos.X == x && c.Pos.Y == y);
            return (newFace, cell, direction);
        }

        return (startFace, null, direction);
    }
}