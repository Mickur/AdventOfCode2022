Console.WriteLine("Mickur's Advent of Code 2022 - Day 4!");

// Setup
var input = File.ReadAllLines("input.txt");

var counter1 = 0;
var counter2 = 0;

foreach (var line in input)
{
    var split1 = line.Split(',');

    // Elf 1
    var elf1Split = split1[0].Split('-');
    
    var parseValue = 0;
    foreach (var c in elf1Split[0])
        parseValue = parseValue * 10 + (c - '0');

    var elf1Start = parseValue;
    
    parseValue = 0;
    foreach (var c in elf1Split[1])
        parseValue = parseValue * 10 + (c - '0');

    var elf1Stop = parseValue;

    // Elf 2
    var elf2Split = split1[1].Split('-');
    
    parseValue = 0;
    foreach (var c in elf2Split[0])
        parseValue = parseValue * 10 + (c - '0');

    var elf2Start = parseValue;
    
    parseValue = 0;
    foreach (var c in elf2Split[1])
        parseValue = parseValue * 10 + (c - '0');

    var elf2Stop = parseValue;

    // Part One: ...
    var elfOneCompletelyInTwo = elf2Start <= elf1Start && elf2Stop >= elf1Stop;
    var elfTwoCompletelyInOne = elf1Start <= elf2Start && elf1Stop >= elf2Stop;
    
    if (elfOneCompletelyInTwo || elfTwoCompletelyInOne)
    {
        counter1++;
    }

    // Part Two: ...
    var elfOnePartlyInTwo =
        (elf1Start >= elf2Start && elf1Start <= elf2Stop) ||
        (elf1Stop >= elf2Start && elf1Stop <= elf2Stop);

    var elfTwoPartlyInOne =
        (elf2Start >= elf1Start && elf2Start <= elf1Stop) ||
        (elf2Stop >= elf1Start && elf2Stop <= elf1Stop);

    if (elfOnePartlyInTwo || elfTwoPartlyInOne)
    {
        counter2++;
    }
}
Console.WriteLine(counter1);
Console.WriteLine(counter2);
