using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace Benchmarks.ConsoleApp;

[MemoryDiagnoser]
public class JsonRewriteBenchmarks
{
    // Test data
    private readonly string[] _testJsons = new[]
    {
        """{"queueName":"js-queue-items","connection":"DefaultEndpointsProtocol=https;AccountName=testaccount;AccountKey=fooBarBaz/ldJTxxfuEecPaQS1Oe1iLisb230mrQYUdOAQ+AStnlaT1a==;EndpointSuffix=core.windows.net","type":"queueTrigger","name":"queueTriggerc8fbfb3748","direction":"in"}""",
        """{"queueName":"js-queue-items2","connection":"MyConnection","type":"queueTrigger","name":"queueTrigger92aef41ff8","direction":"in"}""",
        """{"type":"eventGridTrigger","name":"eventGridTrigger69f302e322","direction":"in"}"""
    };

    private readonly string _propertyName = "connection";

    public static string SanitizePropertyValueUsingJObject(string json, string propertyName)
    {
        // Quick check to avoid unnecessary parsing if the property doesn't exist
        if (!json.Contains($"\"{propertyName}\"", StringComparison.OrdinalIgnoreCase))
        {
            return json;
        }

        var jsonObject = JObject.Parse(json);
        
        JProperty foundProperty = null;
        foreach (var prop in jsonObject.Properties())
        {
            if (string.Equals(prop.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            {
                foundProperty = prop;
                break;
            }
        }

        if (foundProperty != null)
        {
            // Use the original property name to preserve casing
            jsonObject[foundProperty.Name] = Sanitizer.Sanitize(foundProperty.Value.ToString());
            return jsonObject.ToString(Formatting.None);
        }

        return json;
    }

    [Benchmark]
    public void SanitizeUsingJObject_WithConnection()
    {
        foreach (var json in _testJsons)
        {
            SanitizePropertyValueUsingJObject(json, _propertyName);
        }
    }

    [Benchmark]
    public void SanitizeUsingSTJ_WithConnection()
    {
        foreach (var json in _testJsons)
        {
            MetadataJsonHelper.SanitizePropertyValueInJson(json, _propertyName);
        }
    }

    [Benchmark]
    public string SanitizeUsingJObject_SingleItem()
    {
        return SanitizePropertyValueUsingJObject(_testJsons[0], _propertyName);
    }

    [Benchmark]
    public string SanitizeUsingSTJ_SingleItem()
    {
        return MetadataJsonHelper.SanitizePropertyValueInJson(_testJsons[0], _propertyName);
    }

    [Benchmark]
    public string SanitizeUsingJObject_NoProperty()
    {
        return SanitizePropertyValueUsingJObject(_testJsons[2], _propertyName);
    }

    [Benchmark]
    public string SanitizeUsingSTJ_NoProperty()
    {
        return MetadataJsonHelper.SanitizePropertyValueInJson(_testJsons[2], _propertyName);
    }   

}