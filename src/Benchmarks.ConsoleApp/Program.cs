using BenchmarkDotNet.Running;

namespace Benchmarks.ConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<StringSplitBenchmarks>();
    }
}