using System.Diagnostics;
using AoCUtils;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 14!");

// Setup
var input = File.ReadAllLines("input.txt");

// Part One
var startTime = Stopwatch.GetTimestamp();
var slice = new Derp(input, 500, 0);
var counter = 0;

while (slice.SimulateSand(500, 0) != (-1, -1))
{
    counter++;
}
var elapsedTime = Stopwatch.GetElapsedTime(startTime, Stopwatch.GetTimestamp());
Console.WriteLine($"Finished Part One in {elapsedTime.Milliseconds} ms ({elapsedTime.Ticks} ticks)");
Console.WriteLine($"Part One Answer: {counter}");

// Part Two
startTime = Stopwatch.GetTimestamp();
slice = new Derp(input, 500, 0);
counter = 0;
while (slice.SimulateSand(500, 0, true) != (500, 0))
{
    counter++;
}
elapsedTime = Stopwatch.GetElapsedTime(startTime, Stopwatch.GetTimestamp());
Console.WriteLine($"Finished Part Two in {elapsedTime.Milliseconds} ms ({elapsedTime.Ticks} ticks)");
Console.WriteLine($"Part Two Answer: {counter + 1}");

class Derp
{
    private Dictionary<(int, int), char> slice = new ();

    private int _minX = 0;
    private int _maxX = 0;
    private readonly int _minY = 0;
    private readonly int _maxY = 0;

    public Derp(string[] input, int sandX, int sandY)
    {
        _minX = sandX;
        _maxX = sandX;
        _minY = sandY;
        _maxY = sandY;
        
        foreach (var line in input)
        {
            var steps = line.Split(" -> ");

            var previous = steps[0].Split(',');
            var previousX = AoCParsing.FastIntParse(previous[0]);
            var previousY = AoCParsing.FastIntParse(previous[1]);
            
            slice.TryAdd((previousX, previousY), '#');
            
            if (previousX < _minX)
                _minX = previousX;
            else if (previousX > _maxX)
                _maxX = previousX;
                
            if (previousY < _minY)
                _minY = previousY;
            else if (previousY > _maxY)
                _maxY = previousY;
            
            for(var i = 1; i < steps.Length; i++)
            {
                var coords = steps[i].Split(',');
                var x = AoCParsing.FastIntParse(coords[0]);
                var y = AoCParsing.FastIntParse(coords[1]);
                
                if (x < _minX)
                    _minX = x;
                else if (x > _maxX)
                    _maxX = x;

                if (y < _minY)
                    _minY = y;
                else if (y > _maxY)
                    _maxY = y;

                // Same X position
                if (x == previousX)
                {
                    if (y < previousY)
                    {
                        for (var a = previousY - 1; a >= y; a--)
                        {
                            slice.TryAdd((x, a), '#');
                            previousY = a;
                        }
                    }
                    else if(y > previousY)
                    {
                        for (var a = previousY + 1; a <= y; a++)
                        {
                            slice.TryAdd((x, a), '#');
                            previousY = a;
                        }
                    }
                }
                
                // Iterate Y
                else if (y == previousY)
                {
                    if (x < previousX)
                    {
                        for (var a = previousX - 1; a >= x; a--)
                        {
                            slice.TryAdd((a, y), '#');
                            previousX = a;
                        }
                    }
                    else if(x > previousX)
                    {
                        for (var a = previousX + 1; a <= x; a++)
                        {
                            slice.TryAdd((a, y), '#');
                            previousX = a;
                        }
                    }
                }
            }
        }
    }

    public (int, int) SimulateSand(int x, int y, bool partTwo = false)
    {
        while (true)
        {
            // If outside of grid, we're free falling in part one
            if (!partTwo)
            {
                if (x < _minX || x > _maxX || y < _minY || y > _maxY) return (-1, -1);
            }

            // We hit imaginary floor
            if (partTwo && y + 1 == _maxY + 2)
            {
                // If we can't move any more, add location to slice
                slice[(x, y)] = 'o';
                if (x < _minX) _minX = x;
                if (x > _maxX) _maxX = x;
                return (x, y);
            }
            
            // We hit something
            if (slice.ContainsKey((x, y + 1)))
            {
                // Can we move down to the left?
                if (!slice.ContainsKey((x - 1, y + 1)))
                {
                    x -= 1;
                    y += 1;
                    continue;
                }

                // Can we move down to the right?
                if (!slice.ContainsKey((x + 1, y + 1)))
                {
                    x += 1;
                    y += 1;
                    continue;
                }

                // If we can't move any more, add location to slice
                slice[(x, y)] = 'o';
                return (x, y);
            }
            
            // Continue falling down
            y += 1;
        }
    }

    public void DrawSlice()
    {
        for (var y = _minY; y <= _maxY + 1; y++)
        {
            for (var x = _minX; x <= _maxX; x++)
            {
                if(slice.TryGetValue((x, y), out var result))
                    Console.Write(result);
                else
                    Console.Write('.');
            }
            
            Console.Write(Environment.NewLine);
        }
    }
}
