using System.Diagnostics;
using AoCUtils;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 11!");

// Setup
var input = File.ReadAllLines("input.txt");

var sw = new Stopwatch();
sw.Start();

// Part One
var monkeys = ParseMonkeys(input);
RunPartOneRounds(monkeys, 20);
SortMonkeys(monkeys);
var answerA = monkeys[0].Activity * monkeys[1].Activity;

sw.Stop();
Console.WriteLine($"Finished part one in {sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");
sw.Restart();

// Part Two
monkeys = ParseMonkeys(input);
RunPartTwoRounds(monkeys, 10000);
SortMonkeys(monkeys);
var answerB = monkeys[0].Activity * monkeys[1].Activity;

sw.Stop();
Console.WriteLine($"Finished part two in {sw.ElapsedMilliseconds} ms ({sw.ElapsedTicks} ticks)");

Console.WriteLine($"Part One: {answerA}");
Console.WriteLine($"Part Two: {answerB}");

List<Monkey> ParseMonkeys(IReadOnlyList<string> input)
{
    var monkeyList = new List<Monkey>();
    
    for (var i = 0; i < input.Count; i++)
    {
        // If it starts with an M, it's a new monkey
        if (input[i][0] == 'M')
        {
            var monkey = new Monkey();
        
            // Parse items
            var itemsString = input[i + 1].AsSpan(18).ToString();
            var items = itemsString.Split(", ");
            for (var j = 0; j < items.Length; j++)
            {
                monkey.Items.Enqueue(ulong.Parse(items[j]));
            }
        
            // Parse operation
            var operation = input[i + 2][23];
            var operationValue = input[i + 2][25] == 'o' ? 0 : ulong.Parse(input[i + 2].AsSpan(25)); // Set to 0 if "old", otherwise parse it
            monkey.Operation = (operation, operationValue);
        
            // Parse test
            monkey.TestDivision = ulong.Parse(input[i + 3].AsSpan(21));
            monkey.TestTargets = (AoCParsing.FastIntParse(input[i + 4].AsSpan(29)) , AoCParsing.FastIntParse(input[i + 5].AsSpan(30)));

            monkeyList.Add(monkey);

            i += 6;
        }
    }

    return monkeyList;
}

void RunPartOneRounds(IReadOnlyList<Monkey> monkeys, int rounds = 20)
{
    for (var i = 0; i < rounds; i++)
    {
        // Monkeys
        for (var j = 0; j < monkeys.Count; j++)
        {
            // Items
            while (monkeys[j].Items.Count != 0)
            {
                var item = monkeys[j].Inspect();
                monkeys[item.Item2].Items.Enqueue(item.Item1);
            }
        }
    }
}

void RunPartTwoRounds(IReadOnlyList<Monkey> monkeys, int rounds = 10000)
{
    for (var i = 0; i < rounds; i++)
    {
        // Monkeys
        for (var j = 0; j < monkeys.Count; j++)
        {
            // Get product of all division tests
            ulong testsProduct = 1;
            for (int k = 0; k < monkeys.Count; k++)
            {
                testsProduct *= monkeys[k].TestDivision;
            }
        
            // Items
            while (monkeys[j].Items.Count != 0)
            {
                var item = monkeys[j].Inspect(testsProduct);
                monkeys[item.Item2].Items.Enqueue(item.Item1);
            }
        }
    }
}

void SortMonkeys(List<Monkey> monkeys)
{
    monkeys.Sort(delegate(Monkey m1, Monkey m2)
    {
        if (m1.Activity < m2.Activity)
            return 1;
    
        if (m1.Activity > m2.Activity)
            return -1;
    
        return 0;
    });
}

class Monkey
{
    public readonly Queue<ulong> Items = new();
    public (char, ulong) Operation;
    public ulong TestDivision = 0;
    public (int, int) TestTargets;
    public ulong Activity = 0;

    public (ulong, int) Inspect(ulong divideBy = 0)
    {
        Activity++;

        var item = Items.Dequeue();

        switch (Operation.Item1)
        {
            case '+':
                item += Operation.Item2 == 0 ? item : Operation.Item2;
                break;

            case '*':
                item *= Operation.Item2 == 0 ? item : Operation.Item2;
                break;
        }

        if (divideBy > 0)
        {
            item %= divideBy;
        }
        else
        {
            item /= 3;
        }

        return item % TestDivision == 0 ? (item, TestTargets.Item1) : (item, TestTargets.Item2);
    }
}
