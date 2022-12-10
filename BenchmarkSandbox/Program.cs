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
        
        private int toBeAdded = 0;
        private bool shouldBeAdded = false;

        private int register = 1;
        private int cycles = 0;
        private int signalStrengthSum = 0;

        private char[,] crt = new char[40, 6];
        
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
        public (int, string) SolveAB()
        {
            // Loop through each move in the input
            for (var i = 0; cycles < 240; i++)
            {
                // Cycle Start
                cycles++;

                AddSignalStrength();
                UpdateCrtArray();
    
                // 2 cycle operation
                if (input[i] != "noop")
                {
                    toBeAdded = Parsing.FastIntParse(input[i].AsSpan(5));
                    shouldBeAdded = true;
        
                    cycles++;

                    AddSignalStrength();
                    UpdateCrtArray();
                }
    
                // Add values
                if (shouldBeAdded)
                {
                    register += toBeAdded;
                    shouldBeAdded = false;
                }
            }
            
            return (signalStrengthSum, CrtToString());
        }

        void AddSignalStrength()
        {
            if (cycles == 20 || (cycles + 20) % 40 == 0)
            {
                signalStrengthSum += register * cycles;
            }
        }

        void UpdateCrtArray()
        {
            var x = (cycles - 1) % 40;
            var y = cycles / 40;

            if (x >= 40 || y >= 6) return;
    
            if (Math.Abs(register - x) <= 1)
                crt[x, y] = '#';
            else 
                crt[x, y] = '.';
        }

        string CrtToString()
        {
            var builder = new StringBuilder();
    
            for (var y = 0; y < 6; y++)
            {
                for (var x = 0; x < 40; x++)
                {
                    builder.Append(crt[x, y]);
                }
                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}
