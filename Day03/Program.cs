Console.WriteLine("Mickur's Advent of Code 2022 - Day 3!");

// Setup
var wrongList = new List<int>();

// Part One: ...
foreach (var line in File.ReadLines("input.txt"))
{
    var firstCompartment = new List<int>();
    var secondCompartment = new List<int>();
    
    int compartmentlength = line.Length / 2;

    for (int i = 0; i < line.Length; i++)
    {
        int characterValue = Char.IsLower(line[i]) ? line[i] - 'a' + 1 : line[i] - 'A' + 27;
        
        if (i < compartmentlength)
        {
            firstCompartment.Add(characterValue);
        }
        else
        {
            secondCompartment.Add(characterValue);
        }
    }

    var intersect = firstCompartment.Intersect(secondCompartment);

    foreach (var value in intersect)
    {
        wrongList.Add(value);
    }
}

Console.WriteLine(wrongList.Sum());

// Part Two: ...
var badgeList = new List<int>();

var firstElfList = new List<int>();
var secondElfList = new List<int>();
var thirdElfList = new List<int>();

var currentLine = 0;

foreach (var line in File.ReadLines("input.txt"))
{
    var currentElf = currentLine % 3;
    
    for (int i = 0; i < line.Length; i++)
    {
        var characterValue = char.IsLower(line[i]) ? line[i] - 'a' + 1 : line[i] - 'A' + 27;
        
        switch (currentElf)
        {
            case 0:
                firstElfList.Add(characterValue);
                break;
            
            case 1:
                secondElfList.Add(characterValue);
                break;
            
            case 2:
                thirdElfList.Add(characterValue);
                break;
        }
    }

    if (currentElf == 2)
    {
        // Find intersections
        var intersectOne = firstElfList.Intersect(secondElfList).ToList();
        var intersectTwo = intersectOne.Intersect(thirdElfList).ToList();
        foreach (var value in intersectTwo)
        {
            badgeList.Add(value);
        }
            
        // Prepare for a new set of elves
        firstElfList = new List<int>();
        secondElfList = new List<int>();
        thirdElfList = new List<int>();
    }

    currentLine++;
}

Console.WriteLine(badgeList.Sum());
