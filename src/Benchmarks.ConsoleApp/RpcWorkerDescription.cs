using Newtonsoft.Json;
using System.Text.Json;

namespace Benchmarks.ConsoleApp
{
    public class WorkerProcessCountOptions
    {
        public int ProcessCount { get; set; } = 1;

        public int MaxProcessCount { get; set; } = 10;

        public TimeSpan? ProcessStartupInterval { get; set; } = TimeSpan.FromSeconds(10);
    }

    public class RpcWorkerDescription
    {
        private List<string> _extensions = new List<string>();

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "defaultRuntimeName")]
        public string DefaultRuntimeName { get; set; }

        [JsonProperty(PropertyName = "defaultRuntimeVersion")]
        public string DefaultRuntimeVersion { get; set; }

        [JsonProperty(PropertyName = "supportedArchitectures")]
        public List<string> SupportedArchitectures { get; set; }

        [JsonProperty(PropertyName = "supportedOperatingSystems")]
        public List<string> SupportedOperatingSystems { get; set; }

        [JsonProperty(PropertyName = "supportedRuntimeVersions")]
        public List<string> SupportedRuntimeVersions { get; set; }

        [JsonProperty(PropertyName = "sanitizeRuntimeVersionRegex")]
        public string SanitizeRuntimeVersionRegex { get; set; }

        /// <summary>
        /// Gets or sets the worker indexing ability for this worker.
        /// </summary>
        [JsonProperty(PropertyName = "workerIndexing")]
        public string WorkerIndexing { get; set; }

        /// <summary>
        /// Gets or sets the supported file extension type. Functions are registered with workers based on extension.
        /// </summary>
        [JsonProperty(PropertyName = "extensions")]
        public List<string> Extensions
        {
            get
            {
                return _extensions;
            }

            set
            {
                if (value != null)
                {
                    _extensions = value;
                }
            }
        }
    }
}

