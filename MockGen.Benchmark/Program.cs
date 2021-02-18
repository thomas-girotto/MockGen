using BenchmarkDotNet.Running;

namespace MockGen.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MockLibraryBenchmark>();
        }
    }
}
