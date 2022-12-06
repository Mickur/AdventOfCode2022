Console.WriteLine("Mickur's Advent of Code 2022 - Day 6!");

// Setup
var input = File.ReadAllLines("input.txt")[0];
const int partOneUniquesNeeded = 4;
const int partTwoUniquesNeeded = 14;
const int maxUniquesNeeded = partTwoUniquesNeeded > partOneUniquesNeeded ? partTwoUniquesNeeded : partOneUniquesNeeded;

var partOneAllUniqueFound = false;
var partOneAnswer = -1;
var partTwoAllUniqueFound = false;
var partTwoAnswer = -1;

for (var i = 0; i < input.Length; i++)
{
    if (partOneAllUniqueFound && partTwoAllUniqueFound)
        break;
    
    uint charBits = 0;
                
    for (var j = 0; j < maxUniquesNeeded; j++)
    {
        if (i + j >= input.Length)
            break;
        
        // Operators
        // & - Apply mask
        // | - Combine
        // ^ - Combine/Toggle, get difference
                    
        // Convert char to an int value and push it to the correct position in the mask
        uint charMask = (uint)1 << (input[i+j] - 'a');
                    
        // Checks if the value has already been seen
        if ((charBits & charMask) > 0)
            break;
                    
        // Add value to the ones we've seen
        charBits |= charMask;
        
        if (!partOneAllUniqueFound && j == partOneUniquesNeeded - 1)
        {
            partOneAnswer = i + partOneUniquesNeeded;
            partOneAllUniqueFound = true;
        }

        if (!partTwoAllUniqueFound && j == partTwoUniquesNeeded - 1)
        {
            partTwoAnswer = i + partTwoUniquesNeeded;
            partTwoAllUniqueFound = true;
        }
    }
}

Console.WriteLine($"Answer to part one: {partOneAnswer}");
Console.WriteLine($"Answer to part two: {partTwoAnswer}");
