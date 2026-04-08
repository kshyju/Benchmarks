using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.WebUtilities;

namespace Benchmarks.ConsoleApp;


[MemoryDiagnoser]
public class KeyGenerationBenchmarks
{

    [Benchmark]
    public  string GenerateApiKey_Optimized()
    {
        Span<byte> bytes = stackalloc byte[32];
        RandomNumberGenerator.Fill(bytes);
        return "mcp_" + WebEncoders.Base64UrlEncode(bytes);
    }

    [Benchmark(Baseline = true)]
    public string GenerateApiKey()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        var base64 = Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        return $"mcp_{base64}";
    }
}
