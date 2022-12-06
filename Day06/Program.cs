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

for (var i = 0; i < input[0].Length; i++)
{
    if (queue.Count < partOneUniquesNeeded)
    {
        queue.Enqueue(input[0][i]);
        continue;
    }
    
    if (queue.Count >= partOneUniquesNeeded)
    {
        queue.Dequeue();
        queue.Enqueue(input[0][i]);
        
        uniqueCheckHashSet.Clear();
        if (queue.All(x => uniqueCheckHashSet.Add(x)))
        {
            Console.WriteLine($"Answer to part one: {i + 1}");
            break;
        }
    }
}

queue.Clear();

for (var i = 0; i < input[0].Length; i++)
{
    if (queue.Count < partTwoUniquesNeeded)
    {
        queue.Enqueue(input[0][i]);
        continue;
    }

    if (queue.Count >= partTwoUniquesNeeded)
    {
        queue.Dequeue();
        queue.Enqueue(input[0][i]);
        
        uniqueCheckHashSet.Clear();
        if (queue.All(x => uniqueCheckHashSet.Add(x)))
        {
            Console.WriteLine($"Answer to part two: {i + 1}");
            break;
        }
    }
}

sw.Stop();
Console.WriteLine($"{sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");
