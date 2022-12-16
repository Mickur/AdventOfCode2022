using System.Diagnostics;
using AoCUtils;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 15!");

// Setup
var input = File.ReadAllLines("input.txt");

var sensors = new List<Sensor>();
var beacons = new List<Beacon>();

// Part one variables
var partOneMinX = int.MaxValue;
var partOneMaxX = int.MinValue;

// Parse input
var startTime = Stopwatch.GetTimestamp();
for(var i = 0; i < input.Length; i++)
{
    input[i] = input[i].Replace("Sensor at x=", "");
    input[i] = input[i].Replace(" closest beacon is at x=", "");
    input[i] = input[i].Replace(" y=", "");

    var split = input[i].Split(':');

    var sensor = split[0].Split(',');
    var beacon = split[1].Split(',');
    
    var sensorX = AoCParsing.FastIntParse(sensor[0]);
    var sensorY = AoCParsing.FastIntParse(sensor[1]);
    
    var beaconX = AoCParsing.FastIntParse(beacon[0]);
    var beaconY = AoCParsing.FastIntParse(beacon[1]);

    var range = ManhattanDistance(sensorX, sensorY, beaconX, beaconY);
    
    // Set min and max
    if (sensorX - range < partOneMinX)
        partOneMinX = sensorX - range;
    if (sensorX + range > partOneMaxX)
        partOneMaxX = sensorX + range;

    sensors.Add(new Sensor(sensorX, sensorY, range));
    beacons.Add(new Beacon(beaconX, beaconY));
}
var stopTime = Stopwatch.GetTimestamp();
Console.WriteLine($"Finished parsing in {Stopwatch.GetElapsedTime(startTime, stopTime).TotalMilliseconds} ms");

startTime = Stopwatch.GetTimestamp();
var partOneAnswer = PartOne(2000000);
stopTime = Stopwatch.GetTimestamp();
Console.WriteLine($"Part one answer: {partOneAnswer}");
Console.WriteLine($"Finished part one in {Stopwatch.GetElapsedTime(startTime, stopTime).TotalMilliseconds} ms");

startTime = Stopwatch.GetTimestamp();
var partTwoAnswer = PartTwo(4000000);
stopTime = Stopwatch.GetTimestamp();
Console.WriteLine($"Part two answer: {partTwoAnswer}");
Console.WriteLine($"Finished part two in {Stopwatch.GetElapsedTime(startTime, stopTime).TotalMilliseconds} ms");

int PartOne(int y)
{
    var counter = 0;

    for (var x = partOneMinX; x <= partOneMaxX; x++)
    {
        var isBeacon = false;
        
        foreach (var beacon in beacons)
        {
            if (beacon.X == x && beacon.Y == y)
            {
                isBeacon = true;
                break;
            }
        }

        if (!isBeacon)
        {
            foreach (var sensor in sensors)
            {
                if (sensor.IsInRange(x, y))
                {
                    counter++;
                    break;
                }
            }
        }
    }

    return counter;
}

long PartTwo(int size)
{
    long returnValue = -1;
    
    var cts = new CancellationTokenSource();
    try
    {
        Parallel.For(0, size + 1, new ParallelOptions { CancellationToken = cts.Token }, y =>
        {
            for (var x = 0; x <= size;)
            {
                var inRange = false;
                var xJump = x + 1;

                for (var i = 0; i < sensors.Count; i++)
                {
                    if (sensors[i].IsInRange(x, y))
                    {
                        inRange = true;

                        // Find possible jump
                        if (x < sensors[i].X)
                        {
                            var possibleJump = sensors[i].JumpForward(y);
                            if (possibleJump > xJump)
                            {
                                xJump = possibleJump;
                            }
                        }
                    }
                }

                if (!inRange)
                {
                    returnValue = x * 4000000L + y;
                    cts.Cancel();
                }

                x = xJump;
            }
        });
    }
    catch
    {
        // ignored
    }

    return returnValue;
}

int ManhattanDistance(int firstX, int firstY, int secondX, int secondY)
{
    return Math.Abs(firstX - secondX) + Math.Abs(firstY - secondY);
}

class Sensor
{
    public readonly int X;
    public readonly int Y;

    private readonly int _range;

    public Sensor(int x, int y, int range)
    {
        X = x;
        Y = y;
        _range = range;
    }

    public bool IsInRange(int x, int y)
    {
        return Math.Abs(X - x) + Math.Abs(Y - y) <= _range;
    }

    public int JumpForward(int y)
    {
        var yDiff = Math.Abs(Y - y);
        return X + _range - yDiff + 1;
    }
}

class Beacon
{
    public readonly int X;
    public readonly int Y;

    public Beacon(int x, int y)
    {
        X = x;
        Y = y;
    }
}
