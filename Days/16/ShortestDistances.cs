namespace Aoc2022.Days._16;

public class ShortestDistances
{
    public static IEnumerable<Tuple<Node, int>> GetDistances(Graph graph, Node start, List<Node> unopened)
    {
        foreach (var node in unopened)
        {
            var dist = BFS(graph, start, node);
            yield return Tuple.Create(node, dist);
        }
    }

    public static int BFS(Graph graph, Node start, Node end)
    {
        // queue for storing the nodes to visit
        Queue<Node> queue = new Queue<Node>();

        // dictionary to store the distances from the start node to each node
        Dictionary<Node, int> distances = new Dictionary<Node, int>();

        // initialize the distances and add the starting node to the queue
        foreach (var node in graph.Nodes)
        {
            distances[node] = int.MaxValue;
        }
        distances[start] = 0;
        queue.Enqueue(start);

        // while there are nodes to visit
        while (queue.Count > 0)
        {
            // get the next node in the queue
            Node current = queue.Dequeue();

            // if we reached the end node, return the distance
            if (current == end)
            {
                return distances[end];
            }

            // visit the neighbors
            foreach (var neighbor in current.GetNeighbors())
            {
                if (distances[neighbor] == int.MaxValue)
                {
                    // update the distance of the neighbor and add it to the queue
                    distances[neighbor] = distances[current] + 1;
                    queue.Enqueue(neighbor);
                }
            }
        }

        // if the end node was not reached, return -1
        return -1;
    }

}