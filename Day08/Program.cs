using System.Diagnostics;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 8!");

var input = File.ReadAllLines("input.txt");

var sw = new Stopwatch();
sw.Start();

var treeGrid = new List<List<int>>();
var counter = 0;
long bestBAnswer = 0;

foreach (var line in input)
{
    var currRow = new List<int>();

    for (var i = 0; i < line.Length; i++)
    {
        currRow.Add(line[i]);
    }
    
    treeGrid.Add(currRow);
}

var height = treeGrid.Count;
var width = treeGrid[0].Count;

for (var currY = 0; currY < height; currY++)
{
    for (var currX = 0; currX < width; currX++)
    {
        var canBeSeenFromTop = true;
        var canBeSeenFromRight = true;
        var canBeSeenFromBottom = true;
        var canBeSeenFromLeft = true;
        
        // Check up and down
        var currValue = treeGrid[currY][currX];

        // A: Check top
        for (var top = 0; top < currY; top++)
        {
            var checkValue = treeGrid[top][currX];
            if (checkValue >= currValue)
            {
                canBeSeenFromTop = false;
                break;
            }
        }
        
        // A: Check right
        for (var right = width - 1; right > currX; right--)
        {
            var checkValue = treeGrid[currY][right];
            if (checkValue >= currValue)
            {
                canBeSeenFromRight = false;
                break;
            }
        }
        
        // A: Check bottom
        for (var bottom = height - 1; bottom > currY; bottom--)
        {
            var checkValue = treeGrid[bottom][currX];
            if (checkValue >= currValue)
            {
                canBeSeenFromBottom = false;
                break;
            }
        }
        
        // A: Check left
        for (var left = 0; left < currX; left++)
        {
            var checkValue = treeGrid[currY][left];
            if (checkValue >= currValue)
            {
                canBeSeenFromLeft = false;
                break;
            }
        }
        
        if (canBeSeenFromTop || canBeSeenFromRight || canBeSeenFromBottom || canBeSeenFromLeft)
            counter++;
        
        
        // B: Check top
        var tempY = currY - 1;
        var topSight = 0;
        while (tempY >= 0)
        {
            if (currValue <= treeGrid[tempY][currX])
            {
                topSight++;
                break;
            }
            
            if (currValue > treeGrid[tempY][currX])
                topSight++;
            else
                break;
            tempY--;
        }
        
        // B: Check bottom
        tempY = currY + 1;
        var bottomSight = 0;
        while (tempY < height)
        {
            if (currValue <= treeGrid[tempY][currX])
            {
                bottomSight++;
                break;
            }
            
            if (currValue > treeGrid[tempY][currX])
                bottomSight++;
            else
                break;
            tempY++;
        }
        
        // B: Check left
        var tempX = currX - 1;
        var leftSight = 0;
        while (tempX >= 0)
        {
            if (currValue <= treeGrid[currY][tempX])
            {
                leftSight++;
                break;
            }
            
            if (currValue > treeGrid[currY][tempX])
                leftSight++;
            else
                break;
            tempX--;
        }
        
        // B: Check right
        tempX = currX + 1;
        var rightSight = 0;
        while (tempX < width)
        {
            if (currValue <= treeGrid[currY][tempX])
            {
                rightSight++;
                break;
            }
            
            if (currValue > treeGrid[currY][tempX])
                rightSight++;
            else
                break;
            tempX++;
        }

        long bValue = topSight * rightSight * bottomSight * leftSight;

        if (bValue > bestBAnswer)
            bestBAnswer = bValue;
    }
}

sw.Stop();
Console.WriteLine($"Finished in {sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");

Console.WriteLine(counter);
Console.WriteLine(bestBAnswer);
