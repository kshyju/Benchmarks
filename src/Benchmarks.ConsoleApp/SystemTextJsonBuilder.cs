using System.Text.Json;

namespace Benchmarks.ConsoleApp
{
    public sealed class SystemTextJsonBuilder
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly string _workerConfigPath = string.Empty;
        public SystemTextJsonBuilder(string workerConfigPath)
        {
            _workerConfigPath = workerConfigPath;
        }

        public RpcWorkerDescription BuildUsingJsonDocumentAndReadText()
        {
            string json = File.ReadAllText(_workerConfigPath);
            
            using var doc = JsonDocument.Parse(json);
            JsonElement workerConfig = doc.RootElement;

            if (workerConfig.TryGetProperty("description", out var workerDescriptionElement))
            {
                return workerDescriptionElement.Deserialize<RpcWorkerDescription>(_jsonSerializerOptions);
            }
            else
            {
                throw new InvalidOperationException("Worker description not found in the JSON.");
            }
        }

        public RpcWorkerDescription BuildUsingJsonDocument()
        {
            byte[] jsonBytes = File.ReadAllBytes(_workerConfigPath);
            if (jsonBytes.Length >= 3 && jsonBytes[0] == 0xEF && jsonBytes[1] == 0xBB && jsonBytes[2] == 0xBF)
            {
                jsonBytes = jsonBytes[3..];
            }
            using var doc = JsonDocument.Parse(jsonBytes);
            JsonElement workerConfig = doc.RootElement;

            if (workerConfig.TryGetProperty("description", out var workerDescriptionElement))
            {
                return workerDescriptionElement.Deserialize<RpcWorkerDescription>(_jsonSerializerOptions);
            }
            else
            {
                throw new InvalidOperationException("Worker description not found in the JSON.");
            }
        }

        public RpcWorkerDescription BuildUsingUtf8JsonReaderWithJsonDocument()
        {
            byte[] jsonBytes = File.ReadAllBytes(_workerConfigPath);

            ReadOnlySpan<byte> jsonSpan = jsonBytes.AsSpan();
            if (jsonSpan.Length >= 3 && jsonSpan[0] == 0xEF && jsonSpan[1] == 0xBB && jsonSpan[2] == 0xBF)
            {
                jsonSpan = jsonSpan.Slice(3); // Skip BOM
            }

            var reader = new Utf8JsonReader(jsonSpan);
            using var doc = JsonDocument.ParseValue(ref reader);
            JsonElement workerConfig = doc.RootElement;

            if (workerConfig.TryGetProperty("description", out var workerDescriptionElement))
            {
                return workerDescriptionElement.Deserialize<RpcWorkerDescription>(_jsonSerializerOptions);
            }
            else
            {
                throw new InvalidOperationException("Worker description not found in the JSON.");
            }
        }

        public RpcWorkerDescription BuildUsingUtf8JsonReaderDirectly()
        {
            ReadOnlySpan<byte> jsonSpan = File.ReadAllBytes(_workerConfigPath).AsSpan();

            if (jsonSpan.StartsWith(stackalloc byte[] { 0xEF, 0xBB, 0xBF }))
            {
                jsonSpan = jsonSpan[3..]; // Skip BOM
            }

            // BuildUsingSystemTextJsonUtf8JsonReaderDirectly reads JSON efficiently without allocations
            var reader = new Utf8JsonReader(jsonSpan, isFinalBlock: true, state: default);

            // Advance the reader to find "description"
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName &&
                    reader.ValueTextEquals("description"u8))
                {
                    reader.Read(); // Move to the value of "description"
                    return JsonSerializer.Deserialize<RpcWorkerDescription>(ref reader, _jsonSerializerOptions)
                           ?? throw new InvalidOperationException("Worker description is null.");
                }
            }

            throw new InvalidOperationException("Worker description not found in the JSON.");
        }
    }
}
