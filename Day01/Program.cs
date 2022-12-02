using System.Diagnostics;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 1!");

// Setup
var stopWatch = new Stopwatch();

var currentElfCalories = 0;
var gold = 0;
var silver = 0;
var bronze = 0;

stopWatch.Start();

foreach (var line in File.ReadLines("input.txt"))
    if (string.IsNullOrWhiteSpace(line))
    {
        // Are we top 3!?
        if (currentElfCalories > bronze)
        {
            // Do we beat gold?
            if (currentElfCalories >= gold)
            {
                bronze = silver;
                silver = gold;
                gold = currentElfCalories;
            }
            // Or do we beat silver?
            else if (currentElfCalories >= silver)
            {
                bronze = silver;
                silver = currentElfCalories;
            }
            // I guess we only beat bronze. Well, it's something!
            else
            {
                bronze = currentElfCalories;
            }
        }

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

stopWatch.Stop();
Console.WriteLine($"Parsed input in {stopWatch.ElapsedMilliseconds} ms ({stopWatch.ElapsedTicks:n0} ticks)");
stopWatch.Restart();

// Part One: Find elf carrying the most
Console.WriteLine($"Part 1: The elf carrying the most calories has {gold} calories");

// Part Two: Find top 3 elves carrying the most
Console.WriteLine($"Part 2: The three elves that are carrying the most calories have {gold + silver + bronze} calories total");

stopWatch.Stop();
Console.WriteLine($"Printed answers in {stopWatch.ElapsedMilliseconds} ms ({stopWatch.ElapsedTicks:n0} ticks)");