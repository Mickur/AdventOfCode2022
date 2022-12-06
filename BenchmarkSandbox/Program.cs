using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace BenchmarkSandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Day6>();
        }
    }

    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net70)]
    public class Day6
    {
        private string[] input;

        [Params(4, 14)]
        public int neededUniquesInARow;

        [GlobalSetup]
        public void Setup()
        {
            input = File.ReadAllLines("input.txt");
        }

        [Benchmark]
        public int Solution()
        {
            var uniqueCheckHashSet = new HashSet<char>(neededUniquesInARow);

            for (var i = 0; i < input[0].Length; i++)
            {
                uniqueCheckHashSet.Clear();
                for (var j = 0; j < neededUniquesInARow; j++)
                {
                    if (i + j >= input[0].Length || uniqueCheckHashSet.Add(input[0][i + j]) == false)
                        break;

                    if (j == neededUniquesInARow - 1)
                    {
                        return i + neededUniquesInARow;
                    }
                }
            }

            return -1;
        }
    }
}
