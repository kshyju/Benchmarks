
using Benchmarks.ConsoleApp;

namespace Benchmarks.Tests
{
    public class RegexTests
    {
        [Fact]
        public void TestRegexBenchmarks()
        {
            string input = "Hello {foo} and {bar}!";
            var regexBenchmarks = new RegexBenchmarks();

            var outputOld = regexBenchmarks.RegexConstructor().Count(input);
            var outputNew = regexBenchmarks.SourceGenRegex().Count(input);

            Assert.Equal(outputOld, outputNew);
        }
    }
}
