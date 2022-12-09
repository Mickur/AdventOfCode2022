using System.Diagnostics;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 8!");

var input = File.ReadAllLines("input.txt");

var sw = new Stopwatch();
sw.Start();

var treeGrid = new List<List<int>>();
var visibleTreesFromOutside = 0;
long highestScenicScore = 0;

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
        
        var currValue = treeGrid[currY][currX];
        
        // Direction: Up
        var tempY = currY - 1;
        var topSight = 0;
        while (tempY >= 0)
        {
            if (currValue <= treeGrid[tempY][currX])
            {
                topSight++;
                canBeSeenFromTop = false;
                break;
            }
            
            if (currValue > treeGrid[tempY][currX])
                topSight++;
            else
                break;
            tempY--;
        }
        
        // Direction: Down
        tempY = currY + 1;
        var bottomSight = 0;
        while (tempY < height)
        {
            if (currValue <= treeGrid[tempY][currX])
            {
                bottomSight++;
                canBeSeenFromBottom = false;
                break;
            }
            
            if (currValue > treeGrid[tempY][currX])
                bottomSight++;
            else
                break;
            tempY++;
        }
        
        // Direction: Left
        var tempX = currX - 1;
        var leftSight = 0;
        while (tempX >= 0)
        {
            if (currValue <= treeGrid[currY][tempX])
            {
                leftSight++;
                canBeSeenFromLeft = false;
                break;
            }
            
            if (currValue > treeGrid[currY][tempX])
                leftSight++;
            else
                break;
            tempX--;
        }
        
        // Direction: Right
        tempX = currX + 1;
        var rightSight = 0;
        while (tempX < width)
        {
            if (currValue <= treeGrid[currY][tempX])
            {
                rightSight++;
                canBeSeenFromRight = false;
                break;
            }
            
            if (currValue > treeGrid[currY][tempX])
                rightSight++;
            else
                break;
            tempX++;
        }
        
        if (canBeSeenFromTop || canBeSeenFromRight || canBeSeenFromBottom || canBeSeenFromLeft)
            visibleTreesFromOutside++;

        long bValue = topSight * rightSight * bottomSight * leftSight;

        if (bValue > highestScenicScore)
            highestScenicScore = bValue;
    }
}

sw.Stop();
Console.WriteLine($"Finished in {sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");

Console.WriteLine($"Part one: {visibleTreesFromOutside} trees are visible from outside the grid");
Console.WriteLine($"Part two: Highest Scenic Score is {highestScenicScore}");
