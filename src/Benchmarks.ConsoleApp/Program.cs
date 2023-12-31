﻿using BenchmarkDotNet.Running;

namespace Benchmarks.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<RegexBenchmarks>();
        }
        // dotnet run -c Release
        // dotnet run -c Release -f net8.0 --filter "*" --runtimes net7.0 net8.0
    }
}