using AoCUtils;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace BenchmarkSandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarker>();
        }
    }

    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net70)]
    public class Benchmarker
    {
        private string[] input;

        [GlobalSetup]
        public void Setup()
        {
            input = File.ReadAllLines("inputs/2022-Day11.txt");
        }
        
        [Benchmark]
        public List<Monkey> Parse()
        {
            return ParseMonkeys(input);
        }
        
        [Benchmark]
        public ulong SolvePartOne()
        {
            var monkeys = ParseMonkeys(input);
            RunRounds(monkeys, 20, false);
            SortMonkeys(monkeys);
            return monkeys[0].Activity * monkeys[1].Activity;
        }

        [Benchmark]
        public ulong SolvePartTwo()
        {
            var monkeys = ParseMonkeys(input);
            RunRounds(monkeys, 10000, true);
            SortMonkeys(monkeys);
            return monkeys[0].Activity * monkeys[1].Activity;
        }

        public List<Monkey> ParseMonkeys(IReadOnlyList<string> input)
        {
            var monkeyList = new List<Monkey>(10);

            for (var i = 0; i < input.Count; i++)
            {
                var monkey = new Monkey();

                // Parse items
                var itemsString = input[i + 1].AsSpan(18).ToString();
                var items = itemsString.Split(", ");
                for (var j = 0; j < items.Length; j++) monkey.Items.Enqueue(AoCParsing.FastULongParse(items[j]));

                // Parse operation
                var operation = input[i + 2][23];
                var operationValue =
                    input[i + 2][25] == 'o'
                        ? 0
                        : AoCParsing.FastULongParse(input[i + 2].AsSpan(25)); // Set to 0 if "old", otherwise parse it
                monkey.Operation = (operation, operationValue);

                // Parse test
                monkey.TestDivision = AoCParsing.FastULongParse(input[i + 3].AsSpan(21));
                monkey.TestTargets = (AoCParsing.FastIntParse(input[i + 4].AsSpan(29)),
                    AoCParsing.FastIntParse(input[i + 5].AsSpan(30)));

                monkeyList.Add(monkey);

                i += 6;
            }

            return monkeyList;
        }

        public void RunRounds(IReadOnlyList<Monkey> monkeys, int rounds, bool isPartTwo)
        {
            ulong divideBy = 3;

            if (isPartTwo)
            {
                divideBy = 1;
                for (var k = 0; k < monkeys.Count; k++) divideBy *= monkeys[k].TestDivision;
            }

            for (var i = 0; i < rounds; i++)
                // Monkeys
            for (var j = 0; j < monkeys.Count; j++)
                // Items
                while (monkeys[j].Items.Count != 0)
                {
                    var item = monkeys[j].Inspect(isPartTwo, divideBy);
                    monkeys[item.Item2].Items.Enqueue(item.Item1);
                }
        }

        public void SortMonkeys(List<Monkey> monkeys)
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
    }
}

public class Monkey
{
    public readonly Queue<ulong> Items = new(10);
    public ulong Activity;
    public (char, ulong) Operation;
    public ulong TestDivision;
    public (int, int) TestTargets;

    public (ulong, int) Inspect(bool isPartTwo, ulong divideBy)
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

        if (isPartTwo)
            item %= divideBy;
        else
            item /= divideBy;

        return item % TestDivision == 0 ? (item, TestTargets.Item1) : (item, TestTargets.Item2);
    }
}
