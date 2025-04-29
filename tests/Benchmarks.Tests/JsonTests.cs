
using Benchmarks.ConsoleApp;

namespace Benchmarks.Tests
{
    public class StringTests
    {
        [Theory]
        [InlineData("cat,dog,fish", "dog", ',', StringComparison.Ordinal, true)]
        [InlineData("cat,dog,fish", "Dog", ',', StringComparison.Ordinal, false)]
        [InlineData("cat,dog,fish", "bird", ',', StringComparison.Ordinal, false)]
        [InlineData("dog", "dog", ',', StringComparison.Ordinal, true)]
        [InlineData("dog", "cat", ',', StringComparison.Ordinal, false)]
        [InlineData("", "dog", ',', StringComparison.Ordinal, false)]
        [InlineData(null, "dog", ',', StringComparison.Ordinal, false)]
        [InlineData("cat;dog;fish", "dog", ',', StringComparison.Ordinal, false)] 
        [InlineData("cat;dog;fish", "dog", ';', StringComparison.Ordinal, true)] 
        [InlineData("  cat ,  dog ,fish  ", "dog", ',', StringComparison.Ordinal, true)] 
        [InlineData("cat,dog,fish", "Dog", ',', StringComparison.OrdinalIgnoreCase, true)]
        [InlineData("cat,dog,fish", "FISH", ',', StringComparison.OrdinalIgnoreCase, true)]
        [InlineData("cat,dog,fish", "BIRD", ',', StringComparison.OrdinalIgnoreCase, false)]
        public void TestContainsDelimitedValue(
                string input,
                string target,
                char delimiter,
                StringComparison comparison,
                bool expected)
        {
            var result = input.ContainsDelimitedValue(target, delimiter, comparison);
            Assert.Equal(expected, result);
        }
    }
}
