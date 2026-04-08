//using Benchmarks.ConsoleApp;

//namespace Benchmarks.Tests;

//public class DictionarySerializationBenchmarksTests
//{
//    private readonly DictionarySerializationBenchmarks _benchmarks;

//    public DictionarySerializationBenchmarksTests()
//    {
//        _benchmarks = new DictionarySerializationBenchmarks();
//        _benchmarks.Setup();
//    }

//    [Fact]
//    public void SmallDictionary_ReflectionAndSourceGenerated_ReturnSameValue()
//    {
//        var reflectionResult = _benchmarks.ReflectionBased_Small();
//        var sourceGenResult = _benchmarks.SourceGenerated_Small();

//        Assert.Equal(reflectionResult, sourceGenResult);
//    }

//    [Fact]
//    public void LargeDictionary_ReflectionAndSourceGenerated_ReturnSameValue()
//    {
//        var reflectionResult = _benchmarks.ReflectionBased_Large();
//        var sourceGenResult = _benchmarks.SourceGenerated_Large();

//        Assert.Equal(reflectionResult, sourceGenResult);
//    }
//}
