
using Benchmarks.ConsoleApp;

namespace Benchmarks.Tests
{
    public class JsonTests
    {
        [Fact]
        public void EnsureResultsAreSame()
        {
            var bechmarkManager = new JsonSerializationBenchmarks();

            var a = bechmarkManager.UsingNewtonsoftJsonNet();
            var e = bechmarkManager.BuildUsingSystemTextJsonDocumentAndReadText();
            var b = bechmarkManager.BuildUsingSystemTextJsonDocument();
            var c = bechmarkManager.BuildUsingSystemTextJsonUtf8JsonReaderWithJsonDocument();
            var d = bechmarkManager.BuildUsingSystemTextJsonUtf8JsonReaderDirectly();

            Assert.Equal(a.Language, b.Language);
            Assert.Equal(a.Language, c.Language);
            Assert.Equal(a.Language, d.Language);
            Assert.Equal(a.Language, e.Language);
        }
    }
}
