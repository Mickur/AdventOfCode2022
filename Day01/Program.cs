Console.WriteLine("Mickur's Advent of Code 2022 - Day 1!");

// Setup
var input = File.ReadAllLines("input.txt");

var currentElfCalories = 0;
var placement = new int[3];

for (var i = 0; i <= input.Length; i++)
{
    // Since ReadAllLines ignores the last empty line, just check if i is out of range, ezpz.
    if (i >= input.Length || string.IsNullOrWhiteSpace(input[i]))
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
        currentElfCalories += AoCUtils.Parsing.FastIntParse(input[i]);
    }
}

// Part One: Find elf carrying the most
Console.WriteLine($"Part 1: The elf carrying the most calories has {placement[0]} calories");

// Part Two: Find top 3 elves carrying the most
Console.WriteLine($"Part 2: The three elves that are carrying the most calories have {placement[0] + placement[1] + placement[2]} calories total");
