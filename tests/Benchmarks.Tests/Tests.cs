using Benchmarks.ConsoleApp;

namespace Benchmarks.Tests;

public class Tests
{
    [Theory]
    // Tests where the property exists and contains sensitive data that needs sanitization
    [InlineData("""{"queueName":"js-queue-items","connection":"DefaultEndpointsProtocol=https;AccountName=testaccount;AccountKey=fooBarBaz/ldJTxxfuEecPaQS1Oe1iLisb230mrQYUdOAQ+AStnlaT1a==;EndpointSuffix=core.windows.net","type":"queueTrigger","name":"queueTriggerc8fbfb3748","direction":"in"}""", "connection")]
    [InlineData("""{"queueName":"js-queue-items2","connection":"MyConnection","type":"queueTrigger","name":"queueTrigger92aef41ff8","direction":"in"}""", "connection")]

    // Tests where the property exists but does not contain sensitive data, so no sanitization is required
    [InlineData("""{"queueName":"js-queue-items","connection":"DefaultEndpointsProtocol=https;AccountName=testaccount;AccountKey=fooBarBaz/ldJTxxfuEecPaQS1Oe1iLisb230mrQYUdOAQ+AStnlaT1a==;EndpointSuffix=core.windows.net","type":"queueTrigger","name":"queueTriggerc8fbfb3748","direction":"in"}""", "queueName")]
    [InlineData("""{"type":"eventGridTrigger","name":"eventGridTrigger69f302e322","direction":"in"}""", "type")]

    // Tests where the property does not exist in the JSON, ensuring the method takes the fast path and returns the original JSON
    [InlineData("""{"type":"eventGridTrigger","name":"eventGridTrigger69f302e322","direction":"in"}""", "connection")]
    [InlineData("""{"type":"eventGridTrigger","name":"eventGridTrigger69f302e322","direction":"in"}""", "nonExistentProperty")]

    // Case sensitivity tests - both implementations should match property names in a case-insensitive manner
    [InlineData("""{"queueName":"js-queue-items3","connection":"DefaultEndpointsProtocol=https;AccountName=testaccount;AccountKey=fooBarBaz/ldJTxxfuEecPaQS1Oe1iLisb230mrQYUdOAQ+AStnlaT1a==;EndpointSuffix=core.windows.net","type":"queueTrigger","name":"queueTriggerc8fbfb3748","direction":"in"}""", "CONNECTION")] // Case-insensitive match

    // Tests for exact matches and case-insensitive matches
    [InlineData("""{"Connection":"sensitive-value"}""", "Connection")] // Exact match
    [InlineData("""{"Connection":"sensitive-value"}""", "connection")] // Case-insensitive match
    [InlineData("""{"connection":"sensitive-value"}""", "CONNECTION")] // Case-insensitive match
    [InlineData("""{"camelCase":"value","PascalCase":"value","UPPERCASE":"value"}""", "camelCase")] // Exact match
    [InlineData("""{"camelCase":"value","PascalCase":"value","UPPERCASE":"value"}""", "CAMELCASE")] // Case-insensitive match
    public void JsonSanitizationMethodsProduceSameOutput(string json, string propertyName)
    {
        var resultFromJObject = JsonRewriteBenchmarks.SanitizePropertyValueUsingJObject(json, propertyName);
        var resultFromSTJ = JsonRewriteBenchmarks.SanitizePropertyValueUsingSTJ(json, propertyName);
        
        Assert.Equal(resultFromJObject, resultFromSTJ);
    }
}