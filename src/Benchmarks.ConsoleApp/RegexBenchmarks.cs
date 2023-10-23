using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace Benchmarks.ConsoleApp
{
    [HideColumns("Error", "StdDev", "Median", "RatioSD")]
    public class RegexBenchmarks
    {
        [Benchmark(Baseline = true)]
        public Regex RegexConstructor() => new UsingRegexConstructor().ExpressionRegex;

        [Benchmark]
        public Regex SourceGenRegex() => new UsingSourceGenRegex().ExpressionRegex;
    }

    internal class UsingRegexConstructor
    {
        public readonly Regex ExpressionRegex;
        public UsingRegexConstructor()
        {
            ExpressionRegex = new Regex(@"{(.*?)\}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    }

    internal class UsingSourceGenRegex
    {
        public readonly Regex ExpressionRegex;
        public UsingSourceGenRegex()
        {
            ExpressionRegex = RegularExpressions.FunctionBindingAttributeExpressionRegex();
        }
    }

    internal partial class RegularExpressions
    {
        [GeneratedRegex(@"{(.*?)\}", RegexOptions.IgnoreCase)]
        internal static partial Regex FunctionBindingAttributeExpressionRegex();
    }
}
