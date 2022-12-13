using System.Diagnostics;
using AoCUtils;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 13!");

// Setup
var input = File.ReadAllLines("input.txt");
var ParsedInput = new List<object>();
var PartOneAnswer = 0;
var PartTwoAnswer = 0;

var startTime = Stopwatch.GetTimestamp();

// Parsing
foreach (var line in input)
{
    if(!string.IsNullOrWhiteSpace(line))
        ParsedInput.Add(ParseArray(line));
}

var elapsedTime = Stopwatch.GetElapsedTime(startTime, Stopwatch.GetTimestamp());
Console.WriteLine($"Finished parsing in {elapsedTime.Milliseconds} ms ({elapsedTime.Ticks} ticks)");
startTime = Stopwatch.GetTimestamp();

// Part One
var rightIndicies = new List<int>();

for (var i = 0; i < ParsedInput.Count; i += 2)
{
    var leftObjects = ParsedInput[i];
    var rightObjects = ParsedInput[i + 1];

    var result = CompareArrays((List<object>) leftObjects, (List<object>) rightObjects);
    
    if (result == -1)
    {
        rightIndicies.Add((i / 2) + 1);
    }
}

PartOneAnswer = rightIndicies.Sum();

elapsedTime = Stopwatch.GetElapsedTime(startTime, Stopwatch.GetTimestamp());
Console.WriteLine($"Finished part one in {elapsedTime.Milliseconds} ms ({elapsedTime.Ticks} ticks)");
startTime = Stopwatch.GetTimestamp();

// Part Two
/*var newStuff = new List<object>();
foreach (var line in input)
{
    if(!string.IsNullOrWhiteSpace(line))
        newStuff.Add(ParseArray(line));
}*/

var div1 = ParseArray("[[2]]");
var div2 = ParseArray("[[6]]");

//newStuff.Add(div1);
//newStuff.Add(div2);

ParsedInput.Add(div1);
ParsedInput.Add(div2);

ParsedInput.Sort(delegate(object arr1, object arr2)
{
    return CompareArrays((List<object>) arr1, (List<object>) arr2);
});

var div1Index = 0;
var div2Index = 0;

for (int i = 0; i < ParsedInput.Count; i++)
{
    if (ParsedInput[i] == div1)
    {
        div1Index = i + 1;
    }
    
    if (ParsedInput[i] == div2)
    {
        div2Index = i + 1;
    }
}

PartTwoAnswer = div1Index * div2Index;

elapsedTime = Stopwatch.GetElapsedTime(startTime, Stopwatch.GetTimestamp());
Console.WriteLine($"Finished part two in {elapsedTime.Milliseconds} ms ({elapsedTime.Ticks} ticks)");

Console.WriteLine($"Part One answer: {PartOneAnswer}");
Console.WriteLine($"Part Two answer: {PartTwoAnswer}");

List<object> ParseArray(ReadOnlySpan<char> span)
{
    var depth = 0;
    var objects = new List<object>();

    var tempstring = "";

    for (var i = 0; i < span.Length; i++)
    {
        char currChar = span[i];
        
        // int is complete, parse it and save it!
        if (currChar == ',' || currChar == ']')
        {
            if (tempstring != "")
            {
                var value = AoCParsing.FastIntParse(tempstring);
                objects.Add(value);
                tempstring = "";
            }
        }
        
        if (currChar == '[') // This is our array!
        {
            // If we find a new array on our level, parse it!
            if (depth == 1)
            {
                var toAdd = ParseArray(span.Slice(i));
                objects.Add(toAdd);
            }
            
            depth++;
        }
        
        if (currChar == ']') // This is our array!
        {
            if (depth == 1)
                return objects;
            
            depth--;
        }
        
        if (char.IsDigit(currChar) && depth == 1)
        {
            tempstring += currChar;
        }
    }

    return objects;
}

int CompareArrays(List<object> array1, List<object> array2)
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
