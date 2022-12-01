Console.WriteLine("Mickur's Advent of Code 2022 - Day 1!");

// Setup
var elves = new List<int>();
var currentElfCalories = 0;

foreach (var line in File.ReadLines("input.txt"))
    if (string.IsNullOrWhiteSpace(line))
    {
        elves.Add(currentElfCalories);
        currentElfCalories = 0;
    }
    else
    {
        currentElfCalories += int.Parse(line);
    }

if (currentElfCalories > 0)
    elves.Add(currentElfCalories);

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

// Part One: Find elf carrying the most
Console.WriteLine($"Part 1: The elf carrying the most calories has {elves[0]} calories");

// Part Two: Find top 3 elves carrying the most
Console.WriteLine($"Part 2: The three elves that are carrying the most calories have {elves[0] + elves[1] + elves[2]} calories total");