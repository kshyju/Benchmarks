using Benchmarks.ConsoleApp;

namespace Benchmarks.Tests;

public class PlatformBenchmarksTests
{
    [Fact]
    public void GetCurrentPlatform_And_GetCurrentPlatform2_ReturnSameValue()
    {
        var result1 = PlatformBenchmarks.GetCurrentPlatform();
        var result2 = PlatformBenchmarks.GetCurrentPlatform2();

        Assert.Equal(result1, result2);
    }
}
