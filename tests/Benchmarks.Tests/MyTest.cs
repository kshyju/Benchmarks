
using Benchmarks.ConsoleApp;

namespace Benchmarks.Tests
{
    public class MyTest
    {
        [Fact]
        public void DoesNotThrow()
        {
            var benchmark = new ReadLanguageWorkerFileBenchmark();

            benchmark.ReadAllBytesVersion_AllWorkers();
            benchmark.StreamBufferedVersion_AllWorkers();
        }
    }
}
