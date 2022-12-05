Console.WriteLine("Mickur's Advent of Code 2022 - Day 5!");

// Setup
var input = File.ReadAllLines("input.txt");

var stackParseComplete = false;
var instructionsBegin = 0;
var stacks1 = new List<List<char>>();
var stacks2 = new List<List<char>>();

// Parsing...
for (var i = 0; i < input.Length; i++)
{
    if (stackParseComplete)
    {
        instructionsBegin = i + 1;
        break;
    }

    for (var j = 0; j < input[i].Length; j++)
    {
        var valueIndex = j * 4 + 1;
        
        // If valueIndex is out of range, continue.
        if (valueIndex >= input[i].Length)
            continue;
        
        // If valueIndex contains an integer, we are done.
        if (char.IsDigit(input[i][valueIndex]))
        {
            stackParseComplete = true;
            break;
        }
        
        // If we are in the first loop, create a stack to work with
        if (i == 0)
        {
            stacks1.Add(new List<char>());
            stacks2.Add(new List<char>());
        }
        
        // Push value to stacks
        if (input[i][valueIndex] != ' ')
        {
            stacks1[j].Add(input[i][valueIndex]);
            stacks2[j].Add(input[i][valueIndex]);
        }
    }
}

for (var i = 0; i < stacks1.Count; i++)
{
    stacks1[i].Reverse();
    stacks2[i].Reverse();
}

// Actual instructions
for (var i = instructionsBegin; i < input.Length; i++)
{
    var split = input[i].Split(' ');
    var move = AoCUtils.Parsing.FastIntParse(split[1]);
    var from = AoCUtils.Parsing.FastIntParse(split[3]);
    var to = AoCUtils.Parsing.FastIntParse(split[5]);

    // Part One
    for (var j = 0; j < move; j++)
    {
        var lastIndex = stacks1[from - 1].Count - 1;
        var temp = stacks1[from - 1][lastIndex];
        stacks1[from - 1].RemoveAt(lastIndex);
        stacks1[to-1].Add(temp);
        
    }
    
    // Part Two
    for (var j = move; j > 0; j--)
    {
        var currIndex = stacks2[from - 1].Count - j;
        var temp = stacks2[from - 1][currIndex];
        stacks2[from - 1].RemoveAt(currIndex);
        stacks2[to-1].Add(temp);
    }
}

// Part One: ...
var answer = "";
foreach (var stack in stacks1)
{
    answer += stack[^1];
}
Console.WriteLine(answer);

// Part Two: ...
answer = "";
foreach (var stack in stacks2)
{
    answer += stack[^1];
}
Console.WriteLine(answer);
