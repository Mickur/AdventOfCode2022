using System.Diagnostics;
using AoCUtils;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 13!");

// Setup
var input = File.ReadAllLines("input.txt");
var parsedInput = new List<object>();
var partOneAnswer = 0;
var partTwoAnswer = 0;

var startTime = Stopwatch.GetTimestamp();

// Parsing
for (var i = 0; i < input.Length; i++)
    if (input[i] != string.Empty)
        parsedInput.Add(ParseArray(input[i].AsSpan()));

var elapsedTime = Stopwatch.GetElapsedTime(startTime, Stopwatch.GetTimestamp());
Console.WriteLine($"Finished parsing in {elapsedTime.Milliseconds} ms ({elapsedTime.Ticks} ticks)");
startTime = Stopwatch.GetTimestamp();

// Part One
var inputsInRightOrder = new List<int>();

for (var i = 0; i < parsedInput.Count; i += 2)
{
    var result = CompareArrays((List<object>) parsedInput[i], (List<object>) parsedInput[i + 1]);
    
    if (result == -1)
    {
        inputsInRightOrder.Add((i / 2) + 1);
    }
}

partOneAnswer = inputsInRightOrder.Sum();

elapsedTime = Stopwatch.GetElapsedTime(startTime, Stopwatch.GetTimestamp());
Console.WriteLine($"Finished part one in {elapsedTime.Milliseconds} ms ({elapsedTime.Ticks} ticks)");
startTime = Stopwatch.GetTimestamp();

var divider1 = ParseArray("[[2]]");
var divider2 = ParseArray("[[6]]");

parsedInput.Add(divider1);
parsedInput.Add(divider2);

parsedInput.Sort((arr1, arr2) => CompareArrays((List<object>)arr1, (List<object>)arr2));

for (var i = 0; i < parsedInput.Count; i++)
{
    if (parsedInput[i] == divider1)
    {
        partTwoAnswer = i + 1;
    }
    
    if (parsedInput[i] == divider2)
    {
        partTwoAnswer *= i + 1;
        break;
    }
}

elapsedTime = Stopwatch.GetElapsedTime(startTime, Stopwatch.GetTimestamp());
Console.WriteLine($"Finished part two in {elapsedTime.Milliseconds} ms ({elapsedTime.Ticks} ticks)");

Console.WriteLine($"Part One answer: {partOneAnswer}");
Console.WriteLine($"Part Two answer: {partTwoAnswer}");

List<object> ParseArray(ReadOnlySpan<char> span)
{
    var depth = 0;
    var objects = new List<object>();

    var intStartIndex = 0;
    var intLength = 0;

    for (var i = 0; i < span.Length; i++)
    {
        // int is complete, parse it and save it!
        if (span[i] == ',' || span[i] == ']')
        {
            if (intStartIndex > 0)
            {
                var value = AoCParsing.FastIntParse(span.Slice(intStartIndex, intLength));
                objects.Add(value);
                
                // Reset
                intStartIndex = 0;
                intLength = 0;
            }
        }
        
        if (span[i] == '[') // This is our array!
        {
            // If we find a new array on our level, parse it!
            if (depth == 1)
            {
                var toAdd = ParseArray(span[i..]);
                objects.Add(toAdd);
            }
            
            depth++;
        }
        
        if (span[i] == ']') // This is our array!
        {
            if (depth == 1)
                return objects;
            
            depth--;
        }
        
        if (char.IsDigit(span[i]) && depth == 1)
        {
            if (intStartIndex == 0)
                intStartIndex = i;

            intLength++;
        }
    }

    return objects;
}

int CompareArrays(IReadOnlyList<object> array1, IReadOnlyList<object> array2)
{
    var maxLength = Math.Max(array1.Count, array2.Count);
    
    for (var i = 0; i < maxLength; i++)
    {
        // Left is empty, right order!
        if (i >= array1.Count)
            return -1;

        // Right is empty, wrong order!
        if (i >= array2.Count)
            return 1;
        
        // Both are Ints
        if (array1[i] is int && array2[i] is int)
        {
            var a = (int)array1[i];
            var b = (int)array2[i];
            
            if (a < b)
            {
                return -1;
            }
            
            if (a > b)
            {
                return 1;
            }
        }
        // Make sure both are lists
        else
        {
            var a = array1[i] is List<object> ? (List<object>)array1[i] : new List<object> { (int)array1[i] };
            var b = array2[i] is List<object> ? (List<object>)array2[i] : new List<object> { (int)array2[i] };

            var result = CompareArrays(a, b);
            if (result != 0)
                return result;
        }
    }

    return 0;
}
