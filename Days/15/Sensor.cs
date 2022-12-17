namespace Aoc2022.Days._15;

public class Sensor
{
    public Sensor(Point pos, Point beaconPos)
    {
        Pos = pos;
        BeaconPos = beaconPos;
        Distance = pos.ManhattanDistance(beaconPos);
    }

    public long Distance { get; set; }

    public Point Pos { get; set; }
    public Point BeaconPos { get; set; }
}