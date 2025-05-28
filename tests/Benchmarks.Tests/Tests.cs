
using Benchmarks.ConsoleApp;

namespace Benchmarks.Tests;

public class Tests
{
    [Theory]
    [InlineData("", "EnableWorkerIndexing")]
    [InlineData(null, "EnableWorkerIndexing")]
    [InlineData("EnableWorkerIndexing", "EnableWorkerIndexing")]
    [InlineData("featureA,featureB,featureC", "featureC")]
    [InlineData("featureA,featureB,featureC", "featureA")]
    [InlineData("featureA,featureB,featureC", "featureY")]
    [InlineData("featureA,featureB,featureC", "featureD")]
    [InlineData("featureA,featureB,featureC", "featureA,featureB")]
    [InlineData("featureA , featureB , featureC", "featureB")]
    [InlineData("FEATUREA,featureB", "featurea")]

    public void EnsureItWorks(string? input, string value)
    {
        var expected = StringUtils.ContainsUsingStringSplit(input ?? string.Empty, value);
        var actual = StringUtils.ContainsToken(input ?? string.Empty, value);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("one|two|three", "two", '|')]
    [InlineData("one;two;three", "three", ';')]
    [InlineData("one/two/three", "four", '/')]
    public void DifferentDelimiters(string input, string value, char delimiter)
    {
        var expected = StringUtils.ContainsUsingStringSplit(input ?? string.Empty, value, delimiter);
        var actual = StringUtils.ContainsToken(input ?? string.Empty, value, delimiter);
            
        Assert.Equal(expected, actual);
    }
}