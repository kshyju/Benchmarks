
using Benchmarks.ConsoleApp;

namespace Benchmarks.Tests
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            var dict = new Dictionary<string, object>();
            for (int i = 0; i < 50; i++)
            {
                dict[$"Header-{i}"] = $"Value-{i}";
            }

            var original = RpcMessageConversionExtensions.ToRpcDefault(dict);
            var cached = RpcMessageConversionExtensions.ToRpcDefaultWithCachedSerializer(dict);

            Assert.Equal(original, cached);
        }

        [Fact]
        public void Test2()
        {
            var dict = new Dictionary<string, object>
        {
            { "foo", 42 },
            { "bar", 3.14 },
            { "Nested", new Dictionary<string, string> { { "one", "two and three" } } }
        };

            var original = RpcMessageConversionExtensions.ToRpcDefault(dict);
            var cached = RpcMessageConversionExtensions.ToRpcDefaultWithCachedSerializer(dict);

            Assert.Equal(original, cached);
        }
    }
}
