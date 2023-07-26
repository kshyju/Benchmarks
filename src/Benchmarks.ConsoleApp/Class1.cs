using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Benchmarks.ConsoleApp
{
    [MemoryDiagnoser]
    public class ToStringBenchmarks
    {
        IDictionary<string, string> dict = new Dictionary<string, string>();
        [GlobalSetup]
        public void Init()
        {
            dict["a"] = "b";
            dict["b"] = "d";
            dict["c"] = "e";
            dict["d"] = "f";
        }

        [Benchmark]
        public string JsonSerializer_Serialize()
        {
            return JsonSerializer.Serialize(dict);
        }

        [Benchmark]
        public string ManualToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var item in dict)
            {
                sb.Append($"{item.Key}: {item.Value}");
            }
            return sb.ToString();
        }
    }
}
