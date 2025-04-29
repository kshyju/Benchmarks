using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.ConsoleApp
{


    [MemoryDiagnoser]
    public class StringParsingBenchmarks
    {
        private const string Target = "dog";

        private string _shortInput;
        private string _mediumInput;
        private string _largeInput;

        [GlobalSetup]
        public void Setup()
        {
            _shortInput = "cat,dog,fish";
            _mediumInput = string.Join(",", Enumerable.Repeat("cat", 50).Append("dog"));
            _largeInput = string.Join(",", Enumerable.Repeat("cat", 500).Append("dog"));
        }

        [Benchmark(Baseline = true)]
        public bool SplitAndContains_Short() => SplitAndContains(_shortInput);

        [Benchmark]
        public bool ManualSpan_Short() => ManualSpan(_shortInput);

        [Benchmark]
        public bool StringTokenizer_Short() => TokenizerContains(_shortInput);

        [Benchmark]
        public bool SplitAndContains_Medium() => SplitAndContains(_mediumInput);

        [Benchmark]
        public bool ManualSpan_Medium() => ManualSpan(_mediumInput);

        [Benchmark]
        public bool StringTokenizer_Medium() => TokenizerContains(_mediumInput);

        [Benchmark]
        public bool SplitAndContains_Large() => SplitAndContains(_largeInput);

        [Benchmark]
        public bool ManualSpan_Large() => ManualSpan(_largeInput);

        [Benchmark]
        public bool StringTokenizer_Large() => TokenizerContains(_largeInput);

        private static bool SplitAndContains(string input)
        {
            return input.Split(',').Contains(Target);
        }

        private static bool ManualSpan(string input)
        {
            return input.ContainsDelimitedValue(Target, ',');
        }

        private static bool TokenizerContains(string input)
        {
            var tokenizer = new StringTokenizer(input, new[] { ',' });

            foreach (var token in tokenizer)
            {
                if (token.Trim().Equals(Target, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
