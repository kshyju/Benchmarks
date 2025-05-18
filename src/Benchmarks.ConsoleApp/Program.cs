using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Buffers;

namespace Benchmarks.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ReadLanguageWorkerFileBenchmark>();
        }
    }

    [MemoryDiagnoser]
    public class ReadLanguageWorkerFileBenchmark
    {
        private readonly string[] _workerFiles =
        [
            @"C:\\src\\azure-functions-host\\out\\bin\\WebJobs.Script.WebHost\\debug\\workers\\dotnet-isolated\\bin/FunctionsNetHost.exe",
            @"C:\\src\\azure-functions-host\\out\\bin\\WebJobs.Script.WebHost\\debug\\workers\\java\\azure-functions-java-worker.jar",
            @"C:\\src\\azure-functions-host\\out\\bin\\WebJobs.Script.WebHost\\debug\\workers\\node\\dist/src/nodejsWorker.js",
            @"C:\\src\\azure-functions-host\\out\\bin\\WebJobs.Script.WebHost\\debug\\workers\\powershell\\7.4/Microsoft.Azure.Functions.PowerShellWorker.dll",
            @"C:\\src\\azure-functions-host\\out\\bin\\WebJobs.Script.WebHost\\debug\\workers\\python\\3.12/WINDOWS/X64/worker.py"
        ];

        [Benchmark]
        public void ReadAllBytesVersion_AllWorkers()
        {
            foreach (var file in _workerFiles)
            {
                _ = File.ReadAllBytes(file);
            }
        }

        [Benchmark]
        public void StreamBufferedVersion_AllWorkers()
        {
            foreach (var file in _workerFiles)
            {
                const int bufferSize = 4096;
                byte[] buffer = ArrayPool<byte>.Shared.Rent(bufferSize);

                try
                {

                    using var fs = new FileStream(
                        file,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read,
                        bufferSize,
                        FileOptions.SequentialScan);

                    while (fs.Read(buffer, 0, bufferSize) > 0) { }

                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(buffer);
                }
            }
        }
    }
}