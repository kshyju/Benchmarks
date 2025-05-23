using BenchmarkDotNet.Attributes;

namespace Benchmarks.ConsoleApp;

[MemoryDiagnoser]
public class StringSplitBenchmarks
{
    private const string TargetFlag = "featureX";
    private string _shortFlagsPresent;
    private string _shortFlagsAbsent;
    private string _mediumFlagsPresent;
    private string _mediumFlagsAbsent;
    private string _emptyFlags;

    [GlobalSetup]
    public void Setup()
    {
        _shortFlagsPresent = "featureX";
        _shortFlagsAbsent = "feature1";
        _mediumFlagsPresent = "feature1,feature2,featureX,eventLongerFeature4";
        _mediumFlagsAbsent = "feature1,feature2,longFeature3,eventLongerFeature4";
        _emptyFlags = string.Empty;
    }

    [Benchmark(Baseline = true)]
    public bool ContainsToken_Short_Present() => StringUtils.ContainsToken(_shortFlagsPresent, TargetFlag);

    [Benchmark]
    public bool ContainsToken_CheckInline_Short_Present() => StringUtils.ContainsToken_CheckInline(_shortFlagsPresent, TargetFlag);

    [Benchmark]
    public bool ContainsToken_Short_Absent() => StringUtils.ContainsToken(_shortFlagsAbsent, TargetFlag);

    [Benchmark]
    public bool ContainsToken_CheckInline_Short_Absent() => StringUtils.ContainsToken_CheckInline(_shortFlagsAbsent, TargetFlag);

    [Benchmark]
    public bool ContainsToken_Medium_Present() => StringUtils.ContainsToken(_mediumFlagsPresent, TargetFlag);

    [Benchmark]
    public bool ContainsToken_CheckInline_Medium_Present() => StringUtils.ContainsToken_CheckInline(_mediumFlagsPresent, TargetFlag);

    [Benchmark]
    public bool ContainsToken_Medium_Absent() => StringUtils.ContainsToken(_mediumFlagsAbsent, TargetFlag);

    [Benchmark]
    public bool ContainsToken_CheckInline_Medium_Absent() => StringUtils.ContainsToken_CheckInline(_mediumFlagsAbsent, TargetFlag);

    [Benchmark]
    public bool ContainsToken_Empty() => StringUtils.ContainsToken(_emptyFlags, TargetFlag);

    [Benchmark]
    public bool ContainsToken_CheckInline_Empty() => StringUtils.ContainsToken_CheckInline(_emptyFlags, TargetFlag);
}