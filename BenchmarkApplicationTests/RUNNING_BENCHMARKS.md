# ?? Running the Application Layer Benchmarks

## Quick Start

1. **Build in Release mode** (required for accurate benchmarking):
```bash
dotnet build -c Release
```

2. **Run all benchmarks**:
```bash
cd BenchmarkApplicationTests
dotnet run -c Release
```

3. **Select benchmarks interactively** or run specific ones:
```bash
# Run only PlaceOfInterest benchmarks
dotnet run -c Release --filter *PlaceOfInterestBenchmarks*

# Run only validation benchmarks
dotnet run -c Release --filter *ValidatorBenchmarks*
```

## What Gets Benchmarked

### 1. **PlaceOfInterestBenchmarks** - Core Entity Operations
- ? `CreatePlaceOfInterest` - Full create flow with validation
- ? `UpdatePlaceOfInterest` - Update with location changes
- ? `DeletePlaceOfInterest` - Delete operation

### 2. **StartLocationBenchmarks** - Start Location Operations
- ? `CreateStartLocation`
- ? `UpdateStartLocation`
- ? `DeleteStartLocation`

### 3. **EndLocationBenchmarks** - End Location Operations  
- ? `CreateEndLocation`
- ? `UpdateEndLocation`
- ? `DeleteEndLocation`

### 4. **ValidatorBenchmarks** - Validation Performance
- ? `ValidatePlaceOfInterestRequest`
- ? `ValidateStartLocationRequest`
- ? `ValidateEndLocationRequest`

## Understanding Results

Results are saved in `BenchmarkDotNet.Artifacts/results/`:
- **Mean**: Average execution time
- **Error**: Standard error
- **StdDev**: Standard deviation
- **Gen0/Gen1/Gen2**: Garbage collection statistics
- **Allocated**: Memory allocated per operation

### Example Output:
```
| Method                  | Mean     | Error    | StdDev   | Gen0   | Allocated |
|------------------------ |---------:|---------:|---------:|-------:|----------:|
| CreatePlaceOfInterest   | 1.234 ?s | 0.023 ?s | 0.021 ?s | 0.0191 |     120 B |
| UpdatePlaceOfInterest   | 1.156 ?s | 0.019 ?s | 0.018 ?s | 0.0172 |     108 B |
```

## Advanced Options

### Memory Profiling
```bash
dotnet run -c Release --memory
```

### Export Results
```bash
# Export to HTML
dotnet run -c Release --exporters html

# Export to JSON
dotnet run -c Release --exporters json

# Export to Markdown
dotnet run -c Release --exporters md
```

### Run with Specific Job
```bash
# Short run (fast feedback)
dotnet run -c Release --job short

# Medium run (balanced)
dotnet run -c Release --job medium

# Long run (most accurate)
dotnet run -c Release --job long
```

## Tips for Accurate Results

1. **Close unnecessary applications** - Reduce system noise
2. **Disable antivirus temporarily** - Can interfere with timing
3. **Use Release build** - Debug builds have significant overhead
4. **Run multiple times** - First run includes JIT compilation
5. **Keep system idle** - Background tasks affect measurements

## Customizing Benchmarks

To add new benchmarks, create a new class in `BenchmarkApplicationTests/`:

```csharp
[MemoryDiagnoser]
[CPUUsageDiagnoser]
public class MyNewBenchmarks
{
    [GlobalSetup]
    public void Setup()
    {
        // Initialize test data
    }

    [Benchmark]
    public async Task MyBenchmarkedMethod()
    {
        // Method to benchmark
    }
}
```

## Troubleshooting

### Benchmark fails to run
- Ensure you're using Release configuration
- Check that all dependencies are restored: `dotnet restore`

### Results seem inconsistent
- Run with `--job long` for more iterations
- Check for background processes
- Ensure system isn't thermally throttling

### Out of memory errors
- Reduce iteration count in benchmark attributes
- Check for memory leaks in tested code

## Comparing Results

To compare results across code changes:
1. Run benchmarks and save results
2. Make your code changes
3. Run benchmarks again
4. Use BenchmarkDotNet's built-in comparison tools

## CI/CD Integration

For automated benchmarking in pipelines:
```yaml
- name: Run Benchmarks
  run: |
    dotnet run -c Release --project BenchmarkApplicationTests -- --exporters json
```

## More Information

- [BenchmarkDotNet Documentation](https://benchmarkdotnet.org/)
- [Best Practices](https://benchmarkdotnet.org/articles/guides/good-practices.html)
- [Diagnosers](https://benchmarkdotnet.org/articles/configs/diagnosers.html)
