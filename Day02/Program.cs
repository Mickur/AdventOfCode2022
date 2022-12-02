using System.Diagnostics;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 2!");

// Setup
var stopWatch = new Stopwatch();

var points = 0;

const int rockValue = 1;
const int paperValue = 2;
const int scissorValue = 3;

const int lossValue = 0;
const int drawValue = 3;
const int winValue = 6;

stopWatch.Start();

// Part One: Second column is what you're going to play
foreach (var line in File.ReadLines("input.txt"))
    switch (line[0])
    {
        // Enemy plays rock
        case 'A':
            switch (line[2])
            {
                // You play rock
                case 'X':
                    points += rockValue + drawValue;
                    break;

                // You play paper
                case 'Y':
                    points += paperValue + winValue;
                    break;

                // You play scizzor
                case 'Z':
                    points += scissorValue + lossValue;
                    break;
            }

            break;

        // Enemy plays paper
        case 'B':
            switch (line[2])
            {
                // You play rock
                case 'X':
                    points += rockValue + lossValue;
                    break;

                // You play paper
                case 'Y':
                    points += paperValue + drawValue;
                    break;

                // You play scizzor
                case 'Z':
                    points += scissorValue + winValue;
                    break;
            }

            break;

        // Enemy plays scizzor
        case 'C':
            switch (line[2])
            {
                // You play rock
                case 'X':
                    points += rockValue + winValue;
                    break;

                // You play paper
                case 'Y':
                    points += paperValue + lossValue;
                    break;

                // You play scizzor
                case 'Z':
                    points += scissorValue + drawValue;
                    break;
            }

            break;
    }

Console.WriteLine($"Part 1: Your total score was: {points}");

stopWatch.Stop();
Console.WriteLine($"Part 1:  {stopWatch.ElapsedMilliseconds} ms ({stopWatch.ElapsedTicks:n0} ticks)");
stopWatch.Restart();

// Part Two: Seconds column is the result of the game
points = 0;

foreach (var line in File.ReadLines("input.txt"))
    switch (line[0])
    {
        // Enemy plays rock
        case 'A':
            switch (line[2])
            {
                // You should lose
                case 'X':
                    points += scissorValue + lossValue;
                    break;

                // You should draw
                case 'Y':
                    points += rockValue + drawValue;
                    break;

                // You should win
                case 'Z':
                    points += paperValue + winValue;
                    break;
            }

            break;

        case 'B':
            switch (line[2])
            {
                // You should lose
                case 'X':
                    points += rockValue + lossValue;
                    break;

                // You should draw
                case 'Y':
                    points += paperValue + drawValue;
                    break;

                // You should win
                case 'Z':
                    points += scissorValue + winValue;
                    break;
            }

            break;

        case 'C':
            switch (line[2])
            {
                // You should lose
                case 'X':
                    points += paperValue + lossValue;
                    break;

                // You should draw
                case 'Y':
                    points += scissorValue + drawValue;
                    break;

                // You should win
                case 'Z':
                    points += rockValue + winValue;
                    break;
            }

            break;
    }

Console.WriteLine($"Part 2: Your total score was: {points}");

stopWatch.Stop();
Console.WriteLine($"Part 2:  {stopWatch.ElapsedMilliseconds} ms ({stopWatch.ElapsedTicks:n0} ticks)");
