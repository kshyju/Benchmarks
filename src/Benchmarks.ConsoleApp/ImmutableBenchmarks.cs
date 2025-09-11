using BenchmarkDotNet.Attributes;
using System.Collections.Immutable;

namespace Benchmarks.ConsoleApp;

[MemoryDiagnoser]
public class ImmutableBenchmarks
{
    private static readonly string[] EmptyInput = [];
    private static readonly string[] SmallInput = [ "powershell"];
    private static readonly string[] MediumInput = ["java", "powershell", "dotnet-isolated", "python", "csharp"];


    [Benchmark(Baseline = true)]
    public ImmutableDictionary<string, string> ToImmutableDictionary_EmptyInput() => CreateUsingDictionaryThenToImmutableDictionaryCall(EmptyInput);

    [Benchmark]
    public ImmutableDictionary<string, string> ImmutableDictionaryBuilder_EmptyInput() => CreateImmutableDictionaryUsingBuilder(EmptyInput);

    [Benchmark]
    public ImmutableDictionary<string, string> ToImmutableDictionary_SmallInput() => CreateUsingDictionaryThenToImmutableDictionaryCall(SmallInput);

    [Benchmark]
    public ImmutableDictionary<string, string> ImmutableDictionaryBuilder_SmallInput() => CreateImmutableDictionaryUsingBuilder(SmallInput);

    [Benchmark]
    public ImmutableDictionary<string, string> ToImmutableDictionary_MediumInput() => CreateUsingDictionaryThenToImmutableDictionaryCall(MediumInput);

    [Benchmark]
    public ImmutableDictionary<string, string> ImmutableDictionaryBuilder_MediumInput() => CreateImmutableDictionaryUsingBuilder(MediumInput);

    public static ImmutableDictionary<string, string> CreateImmutableDictionaryUsingBuilder(string[] inputs)
    {
        if (inputs == null || inputs.Length == 0)
        {
            return ImmutableDictionary<string, string>.Empty.WithComparers(StringComparer.OrdinalIgnoreCase);
        }

        var builder = ImmutableDictionary.CreateBuilder<string, string>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < inputs.Length; i++)
        {
            var part = inputs[i];
            builder["item_" + i] = part;
        }
        return builder.ToImmutable();
    }

    public static ImmutableDictionary<string, string> CreateUsingDictionaryThenToImmutableDictionaryCall(string[] inputs)
    {
        if (inputs == null || inputs.Length == 0)
        {
            return ImmutableDictionary<string, string>.Empty.WithComparers(StringComparer.OrdinalIgnoreCase);
        }

        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < inputs.Length; i++)
        {
            var part = inputs[i];
            dict["item_" + i] = part;
        }
        return dict.ToImmutableDictionary(StringComparer.OrdinalIgnoreCase);
    }
}