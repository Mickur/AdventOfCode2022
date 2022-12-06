Console.WriteLine("Mickur's Advent of Code 2022 - Day 6!");

// Setup
var input = File.ReadAllLines("input.txt");
const int partOneUniquesNeeded = 4;
const int partTwoUniquesNeeded = 14;
const int maxUniquesNeeded = partTwoUniquesNeeded > partOneUniquesNeeded ? partTwoUniquesNeeded : partOneUniquesNeeded;

var uniqueCheckHashSet = new HashSet<char>(maxUniquesNeeded);
var partOneAllUniqueFound = false;
var partOneAnswer = -1;
var partTwoAllUniqueFound = false;
var partTwoAnswer = -1;

for (var i = 0; i < input[0].Length; i++)
{
    uniqueCheckHashSet.Clear();
    for (var j = 0; j < maxUniquesNeeded; j++)
    {
        if (i + j >= input[0].Length || uniqueCheckHashSet.Add(input[0][i + j]) == false)
            break;

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
    
    if(partOneAllUniqueFound && partTwoAllUniqueFound)
        break;
}

Console.WriteLine($"Answer to part one: {partOneAnswer}");
Console.WriteLine($"Answer to part two: {partTwoAnswer}");
