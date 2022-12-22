namespace Aoc2022.Days._16;

public class TraverseWithPriorityQueue
{
    public int Run(Graph g)
    {
        var flowRates = new Dictionary<string, int>();

        // Initialize the flow rates of all valves to 0
        foreach (var node in g.Nodes)
        {
            flowRates[node.Name] = 0;
        }

        // Initialize a priority queue using a custom comparer that compares the flow rates of the valves
        var priorityQueue = new PriorityQueue(new NodeComparer());
         
        var nodeAA = g.Nodes.Single(x => x.Name == "AA");

        // Add the starting valve (AA) to the priority queue
        priorityQueue.Enqueue(nodeAA);

        var timeRemaining = 30;

        // While there are still valves in the priority queue
        while (priorityQueue.Count > 0)
        {
            // Pop the valve with the highest flow rate from the priority queue
            var currentValve = priorityQueue.Dequeue();
            
            // Update the flow rate of the current valve
            flowRates[currentValve.Name] = currentValve.Value * timeRemaining;
            // For each of the valves connected to the current valve through a tunnel, do the following:
            foreach (var connectedValve in currentValve.GetNeighbors())
            {
                // If the flow rate of the connected valve is higher than the current flow rate, 
                // update the flow rate of the connected valve and add it to the priority queue
                    
                if (connectedValve.Value * timeRemaining  > flowRates[connectedValve.Name])
                {
                    flowRates[connectedValve.Name] = connectedValve.Value * (timeRemaining - 1);
                    priorityQueue.Enqueue(connectedValve);
                }
            }
            timeRemaining--;
            if (timeRemaining <= 0)
            {
                break;
            }
        }
        // The highest flow rate will be stored in the valve with the highest flow rate
        var highestFlowRate = flowRates.Values.Max();

        return highestFlowRate;
    }
}