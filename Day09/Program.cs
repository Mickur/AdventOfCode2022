using System.Diagnostics;
using AoCUtils;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 9!");

// Setup
var input = File.ReadAllLines("input.txt");
const int ropeLength = 10; // Includes the head!
var rope = new RopePart[ropeLength];

// Fill arrays with starting data
for (var i = 0; i < ropeLength; i++)
{
    rope[i] = new RopePart();
}

var sw = new Stopwatch();
sw.Start();

// Loop through each move in the input
foreach (var move in input)
{
    var steps = Parsing.FastIntParse(move.AsSpan(2));

    for (var i = 0; i < steps; i++)
    {
        // Loop through rope
        for (var j = 0; j < ropeLength; j++)
        {
            // Update the position of the rope
            if(j == 0)
                rope[j].Move(move[0]);
            else
                rope[j].Follow(rope[j-1].CurrLocation);
        }
    }
}

sw.Stop();
Console.WriteLine($"Finished in {sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");

// Print the final position of the tails
for (var i = 0; i < ropeLength; i++)
{
    Console.WriteLine(i == 0
        ? $"Head visited: {rope[i].Visited.Count} places"
        : $"Tail {i} visited: {rope[i].Visited.Count} places");
}

Console.ReadKey();

class RopePart
{
    public (int, int) CurrLocation;
    public Dictionary<(int, int), int> Visited;

    public RopePart()
    {
        CurrLocation = (0, 0);
        Visited = new Dictionary<(int, int), int> {{ (0, 0), 1}};
    }

    public void Move(char direction)
    {
        switch (direction)
        {
            case 'U' or 'u':
                CurrLocation.Item2--;
                break;
            case 'D' or 'd':
                CurrLocation.Item2++;
                break;
            case 'L' or 'l':
                CurrLocation.Item1--;
                break;
            case 'R' or 'r':
                CurrLocation.Item1++;
                break;
            default:
                return;
        }
        
        AddVisit(CurrLocation);
    }
    
    public void Follow((int, int) target)
    {
        // Get the current positions of the head and tail
        var thisX = CurrLocation.Item1;
        var thisY = CurrLocation.Item2;
        var targetX = target.Item1;
        var targetY = target.Item2;

        // If we are close enough to our target, don't move
        if (Math.Abs(targetX - thisX) <= 1 && Math.Abs(targetY - thisY) <= 1)
            return;

        // Same X dimension, move in Y dimension
        if (targetX == thisX)
            CurrLocation.Item2 = thisY < targetY ? thisY + 1 : thisY - 1;
    
        // Same Y dimension, move in X dimension
        else if (targetY == thisY)
            CurrLocation.Item1 = thisX < targetX ? thisX + 1 : thisX - 1;

        // Else move diagonally towards head
        else
        {
            CurrLocation.Item1 = thisX > targetX ? thisX - 1 : thisX + 1;
            CurrLocation.Item2 = thisY > targetY ? thisY - 1 : thisY + 1;
        }
        
        AddVisit(CurrLocation);
    }

    private void AddVisit((int, int) location)
    {
        // Add to visited locations
        var key = (CurrLocation.Item1, CurrLocation.Item2);
                
        if (Visited.TryGetValue(key, out var value))
            value++;
        else
        {
            Visited.Add(key, 1);
        }
    }
}
