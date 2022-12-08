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
    //[SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net70)]
    public class Benchmarker
    {
        //private string[] input;

        //[Params(4, 14, 16)]
        //public int neededUniquesInARow;

        [GlobalSetup]
        public void Setup()
        {
            //input = File.ReadAllLines("inputs/2022-Day08.txt");
        }

        [Benchmark]
        public int SolveA()
        {
            return 0;
        }
        
        [Benchmark]
        public int SolveB()
        {
            return 0;
        }
    }
}
