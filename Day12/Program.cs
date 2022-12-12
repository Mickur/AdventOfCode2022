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
        var result = SolveFromLocation(_startX, _startY);
        DrawGridWithPath(result);
        return result.Steps;
    }
    
    public int PartTwo()
    {
        PathStep shortest = null;

        foreach (var startingPoint in _possibleStartingLocations)
        {
            var result = SolveFromLocation(startingPoint.Item1, startingPoint.Item2);

            if (result.Steps >= 0 && result.Steps < (shortest?.Steps ?? int.MaxValue))
            {
                shortest = result;
            }
        }

        if (shortest != null)
        {
            DrawGridWithPath(shortest);
            return shortest.Steps;
        }
        else
        {
            return -1;
        }
    }

    public PathStep SolveFromLocation(int x, int y, bool isPartTwo = false)
    {
        var positionVisited = new HashSet<(int, int)>();
        var queue = new Queue<((int, int), PathStep)>(); // Coordinates and path

        queue.Enqueue(((x, y), new PathStep((x, y), 0)));

        while (queue.Count > 0)
        {
            var positionAndCount = queue.Dequeue();
            var posX = positionAndCount.Item1.Item1;
            var posY = positionAndCount.Item1.Item2;
            var pathStep = positionAndCount.Item2;
            var currLocationValue = _hillGrid[posX, posY];

            // In Part Two, starting value is always 97.
            if (isPartTwo && currLocationValue == 96)
                currLocationValue = 97;
            
            if (posX == _endX && posY == _endY)
            {
                return pathStep;
            }

            if (!positionVisited.Contains((posX, posY)))
            {
                positionVisited.Add((posX, posY));

                // Move up possible?
                if (posY > 0 && currLocationValue >= _hillGrid[posX, posY - 1] - 1)
                    queue.Enqueue(((posX, posY - 1), new PathStep((posX, posY - 1), pathStep.Steps + 1, pathStep)));

                // Move down possible?
                if (posY < _height - 1 && currLocationValue >= _hillGrid[posX, posY + 1] - 1)
                    queue.Enqueue(((posX, posY + 1), new PathStep((posX, posY + 1), pathStep.Steps + 1, pathStep)));

                // Move left possible?
                if (posX > 0 && currLocationValue >= _hillGrid[posX - 1, posY] - 1)
                    queue.Enqueue(((posX - 1, posY), new PathStep((posX - 1, posY), pathStep.Steps + 1, pathStep)));

                // Move right possible?
                if (posX < _width - 1 && currLocationValue >= _hillGrid[posX + 1, posY] - 1)
                    queue.Enqueue(((posX + 1, posY), new PathStep((posX + 1, posY), pathStep.Steps + 1, pathStep)));
            }
        }

        return new PathStep((-1, -1), -1);
    }

    public void DrawGridWithPath(PathStep steps)
    {
        var gridCopy = (int[,])_hillGrid.Clone();
        
        while (steps.Parent != null)
        {
            gridCopy[steps.Coordinates.Item1, steps.Coordinates.Item2] = '#';

            steps = steps.Parent;
        }
        
        for (int x = 0; x < gridCopy.GetLength(1); x++)
        {
            for (int y = 0; y < gridCopy.GetLength(0); y++)
            {
                var character = (char)gridCopy[y, x];

                if (character == '#')
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                if (character == '`')
                    character = 'S';
                
                Console.Write(character);
            }
            Console.Write(Environment.NewLine);
        }
    }
}

public class PathStep
{
    public readonly (int, int) Coordinates;
    public readonly int Steps;
    public readonly PathStep Parent;

    public PathStep((int, int) coords, int steps, PathStep parent = null)
    {
        Coordinates = coords;
        Steps = steps;
        Parent = parent;
    }
}
