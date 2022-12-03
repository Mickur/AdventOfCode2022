Console.WriteLine("Mickur's Advent of Code 2022 - Day 3!");

// Setup
var input = File.ReadAllLines("input.txt");

// Part One: ...
var wrongList = new List<int>();
foreach (var line in input)
{
    var found = false;
    var compartmentLength = line.Length / 2;

    for (var i = 0; i < compartmentLength; i++)
    {
        if (found)
            break;

        for (var j = 0; j < compartmentLength; j++)
        {
            if (found)
                break;

            if (line[i] == line[j + compartmentLength])
            {
                var characterValue = char.IsLower(line[i]) ? line[i] - 'a' + 1 : line[i] - 'A' + 27;
                wrongList.Add(characterValue);
                found = true;
            }
        }
    }
}

Console.WriteLine($"Part 1: {wrongList.Sum()}");

// Part Two: ...
var badgeList = new List<int>();

for (var i = 0; i < input.Length; i+=3)
{
    foreach (var character in input[i])
        if (input[i+1].Contains(character) && input[i+2].Contains(character))
        {
            var characterValue = char.IsLower(character) ? character - 'a' + 1 : character - 'A' + 27;
            badgeList.Add(characterValue);
            break;
        }
}

Console.WriteLine($"Part 2: {badgeList.Sum()}");
