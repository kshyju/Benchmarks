
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

            var regexOld = regexBenchmarks.RegexConstructor();
            var regexNew = regexBenchmarks.SourceGenRegex();

            Assert.Equal(regexOld.IsMatch(input), regexNew.IsMatch(input));
            Assert.Equal(regexOld.Count(input), regexNew.Count(input));
        }
    }
}
