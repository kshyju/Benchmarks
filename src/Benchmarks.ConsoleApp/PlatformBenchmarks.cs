using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;

namespace Benchmarks.ConsoleApp;

[MemoryDiagnoser]
public class PlatformBenchmarks
{
    [Benchmark(Baseline = true)]
    public OSPlatform IsOSPlatform() => GetCurrentPlatform();

    [Benchmark]
    public OSPlatform IsOSPlatformUsingRuntime() => GetCurrentPlatform2();

    public static OSPlatform GetCurrentPlatform2()
    {
        if (OperatingSystem.IsWindows()) return OSPlatform.Windows;
        if (OperatingSystem.IsLinux()) return OSPlatform.Linux;
        if (OperatingSystem.IsMacOS()) return OSPlatform.OSX;
        if (OperatingSystem.IsFreeBSD()) return OSPlatform.FreeBSD;

        return OSPlatform.Create("Unknown");
    }

    public static OSPlatform GetCurrentPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return OSPlatform.Windows;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return OSPlatform.Linux;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OSPlatform.OSX;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            return OSPlatform.FreeBSD;
        }

        return OSPlatform.Create("Unknown");
    }
}