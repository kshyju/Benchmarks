using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace Benchmarks.ConsoleApp
{
    // [MemoryDiagnoser]
    [HideColumns("Error", "StdDev", "Median", "RatioSD")]
    public class RegexBenchmarks
    {
        [Benchmark(Baseline = true)]
        public Regex RegexConstructor() => new MyClassUsingRegexConstructor().ExpressionRegex;

        [Benchmark]
        public Regex SourceGenRegex() => new MyClassUsingSourceGenRegex().ExpressionRegex;
    }

    internal class MyClassUsingRegexConstructor
    {
        public readonly Regex ExpressionRegex;
        public MyClassUsingRegexConstructor()
        {
            ExpressionRegex = new Regex(@"{(.*?)\}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    }

    internal class MyClassUsingSourceGenRegex
    {
        public readonly Regex ExpressionRegex;
        public MyClassUsingSourceGenRegex()
        {
            ExpressionRegex = RegularExpressions.FunctionBindingAttributeExpressionRegex();
        }
    }

    internal partial class RegularExpressions
    {
        [GeneratedRegex("{(.*?)\\}", RegexOptions.IgnoreCase)]
        internal static partial Regex FunctionBindingAttributeExpressionRegex();
    }
}
