using System.Diagnostics;
using System.Text;
using AoCUtils;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 10!");

// Setup
var input = File.ReadAllLines("input.txt");

var toBeAdded = 0;
var shouldBeAdded = false;

var register = 1;
var cycles = 0;
var signalStrengthSum = 0;

var crt = new char[40, 6];
crt[0, 0] = '#';

var sw = new Stopwatch();
sw.Start();

// Loop through each move in the input
for (var i = 0; cycles < 240; i++)
{
    // Cycle Start
    cycles++;

    AddSignalStrength();
    UpdateCrtArray();
    
    // 2 cycle operation
    if (input[i] != "noop")
    {
        toBeAdded = AoCParsing.FastIntParse(input[i].AsSpan(5));
        shouldBeAdded = true;
        
        cycles++;

        AddSignalStrength();
        UpdateCrtArray();
    }
    
    // Add values
    if (shouldBeAdded)
    {
        register += toBeAdded;
        shouldBeAdded = false;
    }
}

var bAnswer = CrtToString();

sw.Stop();
Console.WriteLine($"Finished in {sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");

Console.WriteLine(signalStrengthSum);
Console.WriteLine(bAnswer);

void AddSignalStrength()
{
    if (cycles == 20 || (cycles + 20) % 40 == 0)
    {
        signalStrengthSum += register * cycles;
    }
}

void UpdateCrtArray()
{
    var x = (cycles - 1) % 40;
    var y = cycles / 40;

    if (x >= 40 || y >= 6) return;
    
    if (Math.Abs(register - x) <= 1)
        crt[x, y] = '#';
    else 
        crt[x, y] = '.';
}

string CrtToString()
{
    var builder = new StringBuilder();
    
    for (var y = 0; y < 6; y++)
    {
        for (var x = 0; x < 40; x++)
        {
            builder.Append(crt[x, y]);
        }
        builder.Append(Environment.NewLine);
    }

    return builder.ToString();
}
