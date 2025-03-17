using Newtonsoft.Json.Linq;

namespace Benchmarks.ConsoleApp
{
    public sealed class JsonNetBuilder
    {
        private readonly string _workerConfigPath = string.Empty;

        public JsonNetBuilder(string workerConfigPath)
        {
            _workerConfigPath = workerConfigPath;
        }

        public RpcWorkerDescription Build()
        {
            var jsonContent = File.ReadAllText(_workerConfigPath);
            JObject workerConfig = JObject.Parse(jsonContent);
            RpcWorkerDescription workerDescription = workerConfig.Property("description").Value.ToObject<RpcWorkerDescription>();
            if (workerDescription == null)
            {
                throw new InvalidOperationException("Failed to deserialize worker description.");
            }
            return workerDescription;
        }
    }
}
