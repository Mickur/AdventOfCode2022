using System.Diagnostics;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 12!");

// Setup
var input = File.ReadAllLines("input.txt");

var sw = new Stopwatch();
sw.Start();

var hill = new Hill(input);

sw.Stop();
Console.WriteLine($"Finished creating the grid in {sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");
sw.Restart();

var partOneAnswer = hill.PartOne();

sw.Stop();
Console.WriteLine($"Finished part one in {sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");
sw.Restart();

var partTwoAnswer = hill.PartTwo();

sw.Stop();
Console.WriteLine($"Finished part two in {sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");

Console.WriteLine($"Part One Answer: {partOneAnswer} steps");
Console.WriteLine($"Part Two Answer: {partTwoAnswer} steps");

public class Hill
{
    private readonly int[,] _hillGrid;
    private readonly int _width;
    private readonly int _height;

    private readonly int _startX;
    private readonly int _startY;

    private readonly int _endX;
    private readonly int _endY;

    private readonly List<(int, int)> _possibleStartingLocations = new List<(int, int)>();

    public Hill(string[] input)
    {
        _width = input[0].Length;
        _height = input.Length;

        _hillGrid = new int[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if(input[y][x] == 'a' || input[y][x] == 'S')
                    _possibleStartingLocations.Add((x, y));
                
                if (input[y][x] == 'S')
                {
                    _startX = x;
                    _startY = y;
                    _hillGrid[x, y] = 96;
                }

                else if (input[y][x] == 'E')
                {
                    _endX = x;
                    _endY = y;
                    _hillGrid[x, y] = 123;
                }

                else
                {
                    _hillGrid[x, y] = input[y][x];
                }
            }
        }
    }

    public int PartOne()
    {
        return SolveFromLocation(_startX, _startY);
    }
    
    public int PartTwo()
    {
        var shortest = int.MaxValue;

        foreach (var startingPoint in _possibleStartingLocations)
        {
            var steps = SolveFromLocation(startingPoint.Item1, startingPoint.Item2);

            if (steps >= 0 && steps < shortest)
                shortest = steps;
        }

        return shortest;
    }

    public int SolveFromLocation(int x, int y, bool isPartTwo = false)
    {
        var positionVisited = new HashSet<(int, int)>();
        var queue = new Queue<((int, int), int)>(); // Coordinates and steps

        queue.Enqueue(((x, y), 0));

        while (queue.Count > 0)
        {
            var positionAndCount = queue.Dequeue();
            var posX = positionAndCount.Item1.Item1;
            var posY = positionAndCount.Item1.Item2;
            var count = positionAndCount.Item2;
            var currLocationValue = _hillGrid[posX, posY];

            // In Part Two, starting value is always 97.
            if (isPartTwo && currLocationValue == 96)
                currLocationValue = 97;
            
            if (posX == _endX && posY == _endY)
            {
                return count;
            }

            if (!positionVisited.Contains((posX, posY)))
            {
                positionVisited.Add((posX, posY));

                // Move up possible?
                if (posY > 0 && currLocationValue >= _hillGrid[posX, posY - 1] - 1)
                    queue.Enqueue(((posX, posY - 1), count + 1));

                // Move down possible?
                if (posY < _height - 1 && currLocationValue >= _hillGrid[posX, posY + 1] - 1)
                    queue.Enqueue(((posX, posY + 1), count + 1));

                // Move left possible?
                if (posX > 0 && currLocationValue >= _hillGrid[posX - 1, posY] - 1)
                    queue.Enqueue(((posX - 1, posY), count + 1));

                // Move right possible?
                if (posX < _width - 1 && currLocationValue >= _hillGrid[posX + 1, posY] - 1)
                    queue.Enqueue(((posX + 1, posY), count + 1));
            }
        }

        return -1;
    }
}
