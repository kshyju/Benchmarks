using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;

namespace Benchmarks.ConsoleApp
{
    [MemoryDiagnoser]
    public class RpcConversionBenchmark
    {
        private Dictionary<string, object> _realisticHeaders;

        [GlobalSetup]
        public void Setup()
        {
            _realisticHeaders = new Dictionary<string, object>
            {
                { "Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7" },
                { "Connection", "keep-alive" },
                { "Host", "localhost:5000" },
                { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36 Edg/136.0.0.0" },
                { "Accept-Encoding", "gzip, deflate, br, zstd" },
                { "Accept-Language", "en-US,en;q=0.9" },
                { "Upgrade-Insecure-Requests", "1" },
                { "sec-ch-ua", "\"Chromium\";v=\"136\", \"Microsoft Edge\";v=\"136\", \"Not.A/Brand\";v=\"99\"" },
                { "sec-ch-ua-mobile", "?0" },
                { "sec-ch-ua-platform", "\"Windows\"" },
                { "Sec-Fetch-Site", "none" },
                { "Sec-Fetch-Mode", "navigate" },
                { "Sec-Fetch-User", "?1" },
                { "Sec-Fetch-Dest", "document" }
            };
        }

        [Benchmark(Baseline = true)]
        public string Original() => RpcMessageConversionExtensions.ToRpcDefault(_realisticHeaders);

        [Benchmark]
        public string Cached() => RpcMessageConversionExtensions.ToRpcDefaultWithCachedSerializer(_realisticHeaders);
    }

    public static class RpcMessageConversionExtensions
    {
        private static readonly JsonSerializer _cachedSerializer = JsonSerializer.CreateDefault();

        public static string ToRpcDefault(this object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.None);
        }

        public static string ToRpcDefaultWithCachedSerializer(this object value)
        {
            using var sw = new StringWriter();
            using var writer = new JsonTextWriter(sw)
            {
                Formatting = Formatting.None
            };

            _cachedSerializer.Serialize(writer, value);
            return sw.ToString();
        }
    }
}
