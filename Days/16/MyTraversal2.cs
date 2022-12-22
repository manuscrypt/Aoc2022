using System.IO.Enumeration;

namespace Aoc2022.Days._16;

public class MyTraversal2
{
    private int FlowRateAtDistance(Node node, int distance, int timeRemaining)
    {
        return node.Value * Math.Max(0, timeRemaining - 1 - distance) ;
    }

    public int Descend(Graph g, Node start, List<Node> unopeneds, int timeRemaining, int flowRate, 
        List<int> maxFlowRates)
    {
        if (timeRemaining > 0)
        {
            foreach (var unopened in unopeneds)
            {
                var distance = ShortestDistances.BFS(g, start, unopened);
                if (timeRemaining - 1 - distance <= 0)
                {
                    continue;
                }

                var potFr = FlowRateAtDistance(unopened, distance, timeRemaining);
                var newFr = flowRate + potFr;
                Descend(g, unopened, unopeneds.Where(x => x != unopened).ToList(),
                    timeRemaining - 1 - distance, newFr, maxFlowRates);
            }
        }

        if (flowRate > 0)
        {
            maxFlowRates.Add(flowRate);
        }

        return flowRate;
    }

    public int DescendB(Graph g, Node[] starts, List<Node> unopeneds, int timeRemaining, int flowRate,
        List<int> maxFlowRates)
    {
        var mfr = 0;
        foreach (var s in starts)
        {
            var tfr = Descend(g, s, unopeneds, timeRemaining, flowRate, maxFlowRates);
            if (tfr > mfr)
            {
                mfr = tfr;
            }
        }

        if (mfr > 0)
        {
            maxFlowRates.Add(flowRate);
        }

        return mfr;
    }


    public List<int> RunA(Graph g)
    {
        var start = g.Nodes.Single(x => x.Name == "AA");
        var other = g.Nodes.Where(x => x.Value > 0).ToList();
        List<int> maxFlowRates = new List<int>();
        Descend(g, start, other, 30, 0, maxFlowRates);
        return maxFlowRates;
    }
    public List<int> RunB(Graph g)
    {
        var start = g.Nodes.Single(x => x.Name == "AA");
        var starts = start.GetNeighbors().ToArray();
        var other = g.Nodes.Where(x => x.Value > 0).ToList();
        List<int> maxFlowRates = new List<int>();
        DescendB(g, starts, other, 26, 0, maxFlowRates);
        
        return maxFlowRates;
    }
}