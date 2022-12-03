Console.WriteLine("Mickur's Advent of Code 2022 - Day 2!");

// Setup
var input = File.ReadAllLines("input.txt");

var partOnePoints = 0;
var partTwoPoints = 0;

const int rockValue = 1;
const int paperValue = 2;
const int scissorValue = 3;

const int lossValue = 0;
const int drawValue = 3;
const int winValue = 6;

// Part One: Second column is what you're going to play
// Part Two: Seconds column is the result of the game
foreach (var line in input)
    switch (line[0])
    {
        // Enemy plays rock
        case 'A':
            switch (line[2])
            {
                case 'X':
                    // You play rock
                    partOnePoints += rockValue + drawValue;

                    // You should lose
                    partTwoPoints += scissorValue + lossValue;
                    break;

                case 'Y':
                    // You play paper
                    partOnePoints += paperValue + winValue;

                    // You should draw
                    partTwoPoints += rockValue + drawValue;
                    break;

                case 'Z':
                    // You play scissor
                    partOnePoints += scissorValue + lossValue;

                    // You should win
                    partTwoPoints += paperValue + winValue;
                    break;
            }

            break;

        // Enemy plays paper
        case 'B':
            switch (line[2])
            {
                case 'X':
                    // You play rock
                    partOnePoints += rockValue + lossValue;

                    // You should lose
                    partTwoPoints += rockValue + lossValue;
                    break;

                case 'Y':
                    // You play paper
                    partOnePoints += paperValue + drawValue;

                    // You should draw
                    partTwoPoints += paperValue + drawValue;
                    break;

                case 'Z':
                    // You play scissor
                    partOnePoints += scissorValue + winValue;

                    // You should win
                    partTwoPoints += scissorValue + winValue;
                    break;
            }

            break;

        // Enemy plays scissor
        case 'C':
            switch (line[2])
            {
                case 'X':
                    // You play rock
                    partOnePoints += rockValue + winValue;

                    // You should lose
                    partTwoPoints += paperValue + lossValue;
                    break;

                case 'Y':
                    // You play paper
                    partOnePoints += paperValue + lossValue;

                    // You should draw
                    partTwoPoints += scissorValue + drawValue;
                    break;

                case 'Z':
                    // You play scissor
                    partOnePoints += scissorValue + drawValue;

                    // You should win
                    partTwoPoints += rockValue + winValue;
                    break;
            }

            break;
    }

Console.WriteLine($"Part 1: Your total score was: {partOnePoints}");
Console.WriteLine($"Part 2: Your total score was: {partTwoPoints}");
