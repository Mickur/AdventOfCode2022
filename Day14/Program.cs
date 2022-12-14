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

    private int minX = 0;
    private int maxX = 0;
    private int minY = 0;
    private int maxY = 0;

    public Derp(string[] input, int sandX, int sandY)
    {
        minX = sandX;
        maxX = sandX;
        minY = sandY;
        maxY = sandY;
        
        foreach (var line in input)
        {
            var steps = line.Split(" -> ");

            var previous = steps[0].Split(',');
            var previousX = AoCParsing.FastIntParse(previous[0]);
            var previousY = AoCParsing.FastIntParse(previous[1]);
            
            slice.TryAdd((previousX, previousY), '#');
            
            if (previousX < minX)
                minX = previousX;
            else if (previousX > maxX)
                maxX = previousX;
                
            if (previousY < minY)
                minY = previousY;
            else if (previousY > maxY)
                maxY = previousY;
            
            for(var i = 1; i < steps.Length; i++)
            {
                var coords = steps[i].Split(',');
                var x = AoCParsing.FastIntParse(coords[0]);
                var y = AoCParsing.FastIntParse(coords[1]);
                
                if (x < minX)
                    minX = x;
                else if (x > maxX)
                    maxX = x;

                if (y < minY)
                    minY = y;
                else if (y > maxY)
                    maxY = y;

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
        if (!partTwo)
        {
            // If outside of grid, we're free falling
            if (x < minX || x > maxX || y < minY || y > maxY)
                return (-1, -1);
        }
        
        // We hit imaginary floor
        if (partTwo && y + 1 == maxY + 2)
        {
            // If we can't move any more, add location to slice
            slice[(x, y)] = 'o';
            if (x < minX)
                minX = x;
            if (x > maxX)
                maxX = x;
            return (x, y);
        }
        else if (slice.ContainsKey((x, y + 1))) // Added imaginary floor
        {
            // We hit something
            // Can we move down to the left?
            if (!slice.ContainsKey((x - 1, y + 1)))
            {
                return SimulateSand(x - 1, y + 1, partTwo);
            }
            
            // Can we move down to the right?
            if (!slice.ContainsKey((x + 1, y + 1)))
            {
                return SimulateSand(x + 1, y + 1, partTwo);
            }
            
            // If we can't move any more, add location to slice
            slice[(x, y)] = 'o';
            return (x, y);
        }
        else
        {
            // Continue falling down
            return SimulateSand(x, y + 1, partTwo);
        }
    }

    public void DrawSlice()
    {
        for (var y = minY; y <= maxY + 1; y++)
        {
            for (var x = minX; x <= maxX; x++)
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
