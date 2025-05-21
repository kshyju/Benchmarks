using BenchmarkDotNet.Running;

namespace Benchmarks.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<RpcConversionBenchmark>();
        }
    }
}