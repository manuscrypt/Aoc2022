using System.Text.RegularExpressions;

namespace Aoc2022.Days._16;


[DebuggerDisplay("{Name} => {FlowRate} => {string.Join(',', Connections)}")]
public class Valve
{
    public string Name { get; set; }
    public int FlowRate { get; set; }
    public List<string> Connections { get; set; }
    public bool IsOpen { get; set; }

    public static Valve Parse(string input)
    {
        var match = Regex.Match(input, @"Valve (?<name>\w+) has flow rate=(?<flowRate>\d+); (tunnel|tunnels) (lead|leads) to (valve|valves) (?<connectedValves>.*)");
        if (match.Success)
        {
            var name = match.Groups["name"].Value;
            var flowRate = int.Parse(match.Groups["flowRate"].Value);
            var connectedValves = match.Groups["connectedValves"].Value.Split(", ").ToList();

            return new Valve
            {
                Name = name,
                FlowRate = flowRate,
                Connections = connectedValves
            };
        }

        throw new FormatException("Invalid input string format for Valve");
    }

    public override string ToString()
    {
        return $"{Name} => {FlowRate} => {string.Join(',', Connections)}";
    }
}