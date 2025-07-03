using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Benchmarks.ConsoleApp;

[MemoryDiagnoser]
public class JsonParseBenchmarks
{
    private readonly string _jsonString;
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
}
