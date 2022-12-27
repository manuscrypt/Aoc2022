namespace Aoc2022.Days._19;

using System.Collections.Generic;

public class Bfs
{
    // Perform BFS on a graph or tree rooted at node root
    public int Search(Blueprint blueprint, Node root)
    {
        int max = 0;
        int count = 0;
        // Create a queue to hold nodes that need to be explored
        Queue<Node> queue = new Queue<Node>();

        // Add the root node to the queue
        queue.Enqueue(root);

        // Mark the root node as visited
        root.Visited = true;

        // Explore the graph or tree using BFS
        while (queue.Count > 0)
        {
            // Remove the next node from the queue
            var current = queue.Dequeue();
            count++;
            
            if (current.TimeRemaining == 0)
            {
                var geodes = current.Resources[ResourceType.Geode];
                if (geodes > max)
                {
                    max = geodes;
                }
            }
            // Process the current node

            // Add the current node's unvisited neighbors to the queue
            foreach (var neighbor in current.GetNeighbors(blueprint))
            {
                if (!neighbor.Visited)
                {
                    queue.Enqueue(neighbor);
                    neighbor.Visited = true;
                }
            }
        }

        Console.WriteLine("visited {0} nodes", count);
        return max;
    }
}

// Node class used in the BFS example