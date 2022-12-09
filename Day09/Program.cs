using System.Diagnostics;
using AoCUtils;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 9!");

// Setup
var input = File.ReadAllLines("input.txt");
const int numOfTails = 9;

// Initialize the positions of the head and tail as a tuple of coordinates
var head = (0, 0);
var tailArray = new (int, int)[numOfTails];
var tailVisitedArray = new Dictionary<string, int>[numOfTails];

// Fill arrays with starting data
for (var i = 0; i < numOfTails; i++)
{
    tailArray[i] = (0, 0);
    tailVisitedArray[i] = new Dictionary<string, int> {{"0x0", 1}};
}

var sw = new Stopwatch();
sw.Start();

// Loop through each move in the input
foreach (var move in input)
{
    var splitMove = move.Split(' ');
    var steps = Parsing.FastIntParse(splitMove[1]);

    for (var i = 0; i < steps; i++)
    {
        // Update the position of the head based on the current move
        head = UpdateHeadPosition(head, move[0]);
        
        // Loop through tails
        for (var j = 0; j < numOfTails; j++)
        {
            var temp = tailArray[j];
            
            if (j == 0)
                tailArray[j] = UpdateTailPosition(head, tailArray[j]);
            else
                tailArray[j] = UpdateTailPosition(tailArray[j-1], tailArray[j]);
            
            // Save visited state
            if (tailArray[j] != temp)
            {
                var key = $"{tailArray[j].Item1}x{tailArray[j].Item2}";

                if (tailVisitedArray[j].TryGetValue(key, out var value))
                    value++;
                else
                {
                    tailVisitedArray[j].Add(key, 1);
                }
            }
        }
    }
}

sw.Stop();
Console.WriteLine($"Finished in {sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");

// Print the final position of the tails
for (var i = 0; i < numOfTails; i++)
{
    Console.WriteLine($"Tail {i + 1} visited: {tailVisitedArray[i].Count} places");
}

(int, int) UpdateHeadPosition((int, int) head, char move)
{
    var x = head.Item1;
    var y = head.Item2;

    switch (move)
    {
        case 'U':
            y--;
            break;
        case 'D':
            y++;
            break;
        case 'L':
            x--;
            break;
        case 'R':
            x++;
            break;
    }

    return (x, y);
}

(int, int) UpdateTailPosition((int, int) head, (int, int) tail)
{
    // Get the current positions of the head and tail
    var headX = head.Item1;
    var headY = head.Item2;
    var tailX = tail.Item1;
    var tailY = tail.Item2;

    // If tail is close enough to head, don't move
    if (Math.Abs(headX - tailX) <= 1 && Math.Abs(headY - tailY) <= 1)
        return tail;

    // Same X dimension, move in Y dimension
    if (headX == tailX)
        tailY = tailY < headY ? headY - 1 : headY + 1;
    
    // Same Y dimension, move in X dimension
    else if (headY == tailY)
        tailX = tailX < headX ? headX - 1 : headX + 1;

    // Else move diagonally towards head
    else
    {
        tailX = headX > tailX ? tailX + 1 : tailX - 1;
        tailY = headY > tailY ? tailY + 1 : tailY - 1;
    }

    // Return the new position of the tail as a tuple of coordinates
    return (tailX, tailY);
}
