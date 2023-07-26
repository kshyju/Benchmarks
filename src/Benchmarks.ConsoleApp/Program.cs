using BenchmarkDotNet.Running;
using System;

namespace Benchmarks.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ToStringBenchmarks>();
        }
    }
}