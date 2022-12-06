using System.Diagnostics;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 6!");

// Setup
var input = File.ReadAllLines("input.txt");
const int partOneUniquesNeeded = 4;
const int partTwoUniquesNeeded = 14;
const int maxUniquesNeeded = partTwoUniquesNeeded > partOneUniquesNeeded ? partTwoUniquesNeeded : partOneUniquesNeeded;

var sw = new Stopwatch();
sw.Start();

var uniqueCheckHashSet = new HashSet<char>();
var partOneAllUniqueFound = false;
var partTwoAllUniqueFound = false;

for (var i = 0; i < input[0].Length; i++)
{
    uniqueCheckHashSet.Clear();
    for (var j = 0; j < maxUniquesNeeded; j++)
    {
        if (uniqueCheckHashSet.Add(input[0][i + j]) == false)
            break;

        if (!partOneAllUniqueFound && j == partOneUniquesNeeded - 1)
        {
            Console.WriteLine($"Answer to part one: {i + partOneUniquesNeeded}");
            partOneAllUniqueFound = true;
        }

        if (!partTwoAllUniqueFound && j == partTwoUniquesNeeded - 1)
        {
            Console.WriteLine($"Answer to part two: {i + partTwoUniquesNeeded}");
            partTwoAllUniqueFound = true;
        }
    }
    
    if(partOneAllUniqueFound && partTwoAllUniqueFound)
        break;
}

sw.Stop();
Console.WriteLine($"{sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");
