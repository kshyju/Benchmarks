using BenchmarkDotNet.Attributes;

namespace Benchmarks.ConsoleApp
{
    [MemoryDiagnoser]
    public class JsonSerializationBenchmarks
    {
        private readonly JsonNetBuilder _jsonNetBuilder;
        private readonly SystemTextJsonBuilder _systemTextJsonBuilder;

        public JsonSerializationBenchmarks()
        {
            var workerConfigPath = Path.Combine(AppContext.BaseDirectory, "worker.config.json");
            if (!File.Exists(workerConfigPath))
            {
                throw new FileNotFoundException($"The file {workerConfigPath} does not exist.");
            }
            _jsonNetBuilder = new JsonNetBuilder(workerConfigPath);
            _systemTextJsonBuilder = new SystemTextJsonBuilder(workerConfigPath);
        }

        [Benchmark(Baseline = true)]
        public RpcWorkerDescription UsingNewtonsoftJsonNet()
        {
            return _jsonNetBuilder.Build();
        }

        [Benchmark]
        public RpcWorkerDescription BuildUsingSystemTextJsonDocumentAndReadText()
        {
            return _systemTextJsonBuilder.BuildUsingJsonDocumentAndReadText();
        }

        [Benchmark]
        public RpcWorkerDescription BuildUsingSystemTextJsonDocument()
        {
            return _systemTextJsonBuilder.BuildUsingJsonDocument();
        }
        [Benchmark]
        public RpcWorkerDescription BuildUsingSystemTextJsonUtf8JsonReaderDirectly()
        {
            return _systemTextJsonBuilder.BuildUsingUtf8JsonReaderDirectly();
        }
        [Benchmark]
        public RpcWorkerDescription BuildUsingSystemTextJsonUtf8JsonReaderWithJsonDocument()
        {
            return _systemTextJsonBuilder.BuildUsingUtf8JsonReaderWithJsonDocument();
        }
    }
}
