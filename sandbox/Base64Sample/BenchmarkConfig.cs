using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace Base64Sample
{
    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            this.Add(MemoryDiagnoser.Default);
            this.Add(MarkdownExporter.GitHub);
            this.Add(Job.ShortRun);
        }
    }
}