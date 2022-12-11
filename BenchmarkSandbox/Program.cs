using System.Globalization;
using System.Text;
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
    //[SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net70)]
    public class Benchmarker
    {
        private string[] input;

        [GlobalSetup]
        public void Setup()
        {
            input = File.ReadAllLines("inputs/2022-Day10.txt");
        }

        /*[Benchmark]
        public int SolveA()
        {
            return 1;
        }*/
        
        /*[Benchmark]
        public int SolveB()
        {
            return 0;
        }*/
        
        [Benchmark]
        public int IntParse()
        {
            return int.Parse("-12345", NumberStyles.Integer, CultureInfo.InvariantCulture) + int.Parse("123456", NumberStyles.Integer, CultureInfo.InvariantCulture);
        }
        
        [Benchmark]
        public int FastIntParse()
        {
            return AoCParsing.FastIntParse("-12345") + AoCParsing.FastIntParse("123456");
        }
        
        [Benchmark]
        public ulong ULongParse()
        {
            return ulong.Parse("18446744073709551614", NumberStyles.Integer, CultureInfo.InvariantCulture);
        }
        
        [Benchmark]
        public ulong FastULongParse()
        {
            return AoCParsing.FastULongParse("18446744073709551614");
        }
    }
}
