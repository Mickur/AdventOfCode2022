Console.WriteLine("Mickur's Advent of Code 2022 - Day 4!");

// Setup
var input = File.ReadAllLines("input.txt");

var counter1 = 0;
var counter2 = 0;

foreach (var line in input)
{
    var parseDestination = 0;
    
    var elf1Start = 0;
    var elf1Stop = 0;
    var elf2Start = 0;
    var elf2Stop = 0;

    var parseValue = 0;
    
    for (var i = 0; i <= line.Length; i++)
    {
        if (i >= line.Length || line[i] is '-' or ',')
        {
            switch (parseDestination)
            {
                case 0:
                    elf1Start = parseValue;
                    break;
                case 1:
                    elf1Stop = parseValue;
                    break;
                case 2:
                    elf2Start = parseValue;
                    break;
                case 3:
                    elf2Stop = parseValue;
                    break;
            }
            parseDestination++;
            parseValue = 0;
        }
        else
        {
            parseValue = parseValue * 10 + (line[i] - '0');
        }
    }

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
