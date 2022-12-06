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
    //[SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net70)]
    public class Day6
    {
        private string input;

        [Params(4, 14, 16)]
        public int neededUniquesInARow;

        [GlobalSetup]
        public void Setup()
        {
            input = File.ReadAllLines("inputs/2022-Day06.txt")[0];
        }

        [Benchmark]
        public int Solution()
        {
            var uniqueCheckHashSet = new HashSet<char>(neededUniquesInARow);

            for (var i = 0; i < input.Length; i++)
            {
                uniqueCheckHashSet.Clear();
                for (var j = 0; j < neededUniquesInARow; j++)
                {
                    if (i + j >= input.Length || uniqueCheckHashSet.Add(input[i + j]) == false)
                        break;

                    if (j == neededUniquesInARow - 1)
                    {
                        return i + neededUniquesInARow;
                    }
                }
            }

            return -1;
        }
        
        [Benchmark]
        public int BitSolution()
        {
            for (var i = 0; i < input.Length - neededUniquesInARow; i++)
            {
                uint charBits = 0;
                
                for (var j = 0; j < neededUniquesInARow; j++)
                {
                    // Operators
                    // & - Apply mask
                    // | - Combine
                    // ^ - Combine/Toggle, get difference
                    
                    // Convert char to an int value and push it to the correct position in the mask
                    uint charMask = (uint)1 << (input[i+j] - 'a');
                    
                    // Checks if the value has already been seen
                    if ((charBits & charMask) > 0)
                        break;
                    
                    // Add value to the ones we've seen
                    charBits |= charMask;
                    
                    // If we got this far in this loop, we're successful!
                    if (j == neededUniquesInARow - 1)
                        return i + neededUniquesInARow;
                }
            }

            return -1;
        }
    }
}
