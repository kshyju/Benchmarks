using BenchmarkDotNet.Attributes;

namespace Benchmarks.ConsoleApp;

[MemoryDiagnoser]
public class StringSplitBenchmarks
{
    private const string InputString = "java|powershell|dotnet-isolated|python";

    [Benchmark(Baseline = true)]
    public HashSet<string> CreateHashSetUsingStringSplit() => StringUtils.CreateHashSetUsingStringSplit(InputString);

    [Benchmark]
    public HashSet<string> CreateHashSetUsingSpan() => StringUtils.CreateHashSetOptimized(InputString);
}