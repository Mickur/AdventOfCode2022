using System.Diagnostics;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 1!");

// Setup
var stopWatch = new Stopwatch();
var elves = new List<int>();
var currentElfCalories = 0;

stopWatch.Start();

foreach (var line in File.ReadLines("input.txt"))
    if (string.IsNullOrWhiteSpace(line))
    {
        elves.Add(currentElfCalories);
        currentElfCalories = 0;
    }
    else
    {
        // Micro-optimization, GOTTA GO FAST!
        // currentElfCalories += int.Parse(line); // Old, super slow code!
        
        var lineValue = 0;
        
        foreach (var c in line)
            lineValue = lineValue * 10 + (c - '0');

        currentElfCalories += lineValue;
    }

if (currentElfCalories > 0)
    elves.Add(currentElfCalories);

stopWatch.Stop();
Console.WriteLine($"Parsed input in {stopWatch.ElapsedMilliseconds} ms ({stopWatch.ElapsedTicks:n0} ticks)");
stopWatch.Restart();

// Note: There are better ways to get the top elves than this. But I don't care, this code is simple! I also assume that the input is correctly formatted and that there are always at least three elves.
elves.Sort((a, b) =>
{
    if (a < b)
    {
        return 1;
    }
    
    if (a > b)
    {
        return -1;
    }
    
    return 0;
});

stopWatch.Stop();
Console.WriteLine($"Sorted elves in {stopWatch.ElapsedMilliseconds} ms ({stopWatch.ElapsedTicks:n0} ticks)");
stopWatch.Restart();

// Part One: Find elf carrying the most
Console.WriteLine($"Part 1: The elf carrying the most calories has {elves[0]} calories");

// Part Two: Find top 3 elves carrying the most
Console.WriteLine($"Part 2: The three elves that are carrying the most calories have {elves[0] + elves[1] + elves[2]} calories total");

stopWatch.Stop();
Console.WriteLine($"Printed answers in {stopWatch.ElapsedMilliseconds} ms ({stopWatch.ElapsedTicks:n0} ticks)");