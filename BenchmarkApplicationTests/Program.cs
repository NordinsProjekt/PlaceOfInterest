using BenchmarkDotNet.Running;

namespace BenchmarkApplicationTests;

internal class Program
{
    static void Main(string[] args)
    {
        // Run all benchmarks in the assembly
        var _ = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        
        // Alternative: Run specific benchmark classes
        // BenchmarkRunner.Run<PlaceOfInterestBenchmarks>(args);
        // BenchmarkRunner.Run<StartLocationBenchmarks>(args);
        // BenchmarkRunner.Run<EndLocationBenchmarks>(args);
        // BenchmarkRunner.Run<ValidatorBenchmarks>(args);
    }
}

