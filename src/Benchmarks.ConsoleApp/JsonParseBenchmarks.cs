using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Benchmarks.ConsoleApp;

[MemoryDiagnoser]
public class JsonParseBenchmarks
{
    private readonly string _jsonString;
    private readonly string _largeJsonString;
    private static readonly JsonSerializerSettings _settings = new()
    {
        DateParseHandling = DateParseHandling.None
    };
    public JsonParseBenchmarks()
    {
        _jsonString = @"{
                ""name"": ""John Doe"",
                ""age"": 30,
                ""isEmployed"": true,
                ""dateOfBirth"": ""2024-04-29T11:35:00+08:00"",
                ""skills"": [""C#"", ""JavaScript"", ""Python""]
            }";

        _largeJsonString = @"{
        ""users"": [
            { ""id"": 1, ""name"": ""Alice"", ""age"": 25, ""email"": ""alice@example.com"" },
            { ""id"": 2, ""name"": ""Bob"", ""age"": 30, ""email"": ""bob@example.com"" },
            { ""id"": 3, ""name"": ""Charlie"", ""age"": 35, ""email"": ""charlie@example.com"" }
        ],
        ""metadata"": {
            ""totalUsers"": 3,
            ""generatedAt"": ""2025-07-03T12:00:00Z""
        }
    }";
    }

    [Benchmark]
    public JObject UseLoad()
    {
        using var stringReader = new StringReader(_jsonString);
        using var jsonReader = new JsonTextReader(stringReader)
        {
            DateParseHandling = DateParseHandling.None
        };

        return JObject.Load(jsonReader);
    }

    [Benchmark]
    public JObject UseSerialize()
    {
        var jsonObject = JsonConvert.DeserializeObject<JObject>(_jsonString, _settings);
        return jsonObject;
    }

    [Benchmark]
    public JObject UseParse()
    {
        return JObject.Parse(_jsonString);
    }

    [Benchmark]
    public JObject UseLoadLarge()
    {
        using var stringReader = new StringReader(_largeJsonString);
        using var jsonReader = new JsonTextReader(stringReader)
        {
            DateParseHandling = DateParseHandling.None
        };

        return JObject.Load(jsonReader);
    }

    [Benchmark]
    public JObject UseSerializeLarge()
    {
        var jsonObject = JsonConvert.DeserializeObject<JObject>(_largeJsonString, _settings);
        return jsonObject;
    }

    [Benchmark]
    public JObject UseParseLarge()
    {
        return JObject.Parse(_largeJsonString);
    }
}
