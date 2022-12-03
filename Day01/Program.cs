Console.WriteLine("Mickur's Advent of Code 2022 - Day 1!");

// Setup
var input = File.ReadAllLines("input.txt");

var currentElfCalories = 0;
var placement = new int[3];

foreach (var line in input)
    if (string.IsNullOrWhiteSpace(line))
    {
        // Are we top 3!?
        if (currentElfCalories > placement[2])
        {
            // Do we beat gold?
            if (currentElfCalories >= placement[0])
            {
                placement[2] = placement[1];
                placement[1] = placement[0];
                placement[0] = currentElfCalories;
            }
            // Or do we beat silver?
            else if (currentElfCalories >= placement[1])
            {
                placement[2] = placement[1];
                placement[1] = currentElfCalories;
            }
            // I guess we only beat bronze. Well, it's something!
            else
            {
                placement[2] = currentElfCalories;
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

// Part One: Find elf carrying the most
Console.WriteLine($"Part 1: The elf carrying the most calories has {placement[0]} calories");

// Part Two: Find top 3 elves carrying the most
Console.WriteLine($"Part 2: The three elves that are carrying the most calories have {placement[0] + placement[1] + placement[2]} calories total");
