using System.Text.Json;
using System.Text.Json.Serialization;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.ConsoleApp;

[JsonSerializable(typeof(IDictionary<string, string>))]
internal partial class DictionaryJsonContext : JsonSerializerContext;

[MemoryDiagnoser]
public class DictionarySerializationBenchmarks
{
    private IDictionary<string, string> _small = null!;
    private IDictionary<string, string> _large = null!;

    [GlobalSetup]
    public void Setup()
    {
        _small = new Dictionary<string, string>
        {
            ["browserName"] = "chrome",
            ["platformName"] = "Windows 11",
            ["acceptInsecureCerts"] = "true"
        };

        _large = new Dictionary<string, string>();
        for (var i = 0; i < 100; i++)
        {
            _large[$"capability_{i}"] = $"value_{i}_{new string('x', 50)}";
        }
    }

    [Benchmark(Baseline = true)]
    public string ReflectionBased_Small() => JsonSerializer.Serialize(_small);

    [Benchmark]
    public string SourceGenerated_Small() => JsonSerializer.Serialize(_small, DictionaryJsonContext.Default.IDictionaryStringString);

    [Benchmark]
    public string ReflectionBased_Large() => JsonSerializer.Serialize(_large);

    [Benchmark]
    public string SourceGenerated_Large() => JsonSerializer.Serialize(_large, DictionaryJsonContext.Default.IDictionaryStringString);
}
