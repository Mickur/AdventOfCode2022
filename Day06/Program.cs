using System.Diagnostics;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 6!");

// Setup
var input = File.ReadAllLines("input.txt");
const int partOneUniquesNeeded = 4;
const int partTwoUniquesNeeded = 14;

var sw = new Stopwatch();
sw.Start();

var queue = new Queue<char>();
var uniqueCheckHashSet = new HashSet<char>();
var allUnique = false;

for (var i = 0; i < input[0].Length; i++)
{
    uniqueCheckHashSet.Clear();
    for (var j = 0; j < partOneUniquesNeeded; j++)
    {
        if (uniqueCheckHashSet.Add(input[0][i + j]) == false)
            break;

        if (j == partOneUniquesNeeded - 1)
            allUnique = true;
    }

    if (allUnique)
    {
        Console.WriteLine($"Answer to part one: {i + partOneUniquesNeeded}");
        break;
    }
}

queue.Clear();
allUnique = false;

for (var i = 0; i < input[0].Length; i++)
{
    uniqueCheckHashSet.Clear();
    for (var j = 0; j < partTwoUniquesNeeded; j++)
    {
        if (uniqueCheckHashSet.Add(input[0][i + j]) == false)
            break;

        if (j == partTwoUniquesNeeded - 1)
            allUnique = true;
    }

    if (allUnique)
    {
        Console.WriteLine($"Answer to part two: {i + partTwoUniquesNeeded}");
        break;
    }
}

sw.Stop();
Console.WriteLine($"{sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");
