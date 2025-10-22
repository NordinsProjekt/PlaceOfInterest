# Application Layer Benchmarks

This project contains comprehensive benchmarks for the PlaceOfInterest Application layer, testing all MediatR handlers and validators.

## Benchmark Classes

### 1. PlaceOfInterestBenchmarks
Tests the core PlaceOfInterest use cases:
- `CreatePlaceOfInterest` - Measures handler + validation performance
- `UpdatePlaceOfInterest` - Measures update operation performance
- `DeletePlaceOfInterest` - Measures delete operation performance

### 2. StartLocationBenchmarks
Tests StartLocation use cases:
- `CreateStartLocation`
- `UpdateStartLocation`
- `DeleteStartLocation`

### 3. EndLocationBenchmarks
Tests EndLocation use cases:
- `CreateEndLocation`
- `UpdateEndLocation`
- `DeleteEndLocation`

### 4. ValidatorBenchmarks
Compares validation performance across different validators:
- `ValidatePlaceOfInterestRequest`
- `ValidateStartLocationRequest`
- `ValidateEndLocationRequest`

## Running Benchmarks

### Run all benchmarks:
```bash
dotnet run -c Release
```

### Run specific benchmark class:
```bash
dotnet run -c Release --filter *PlaceOfInterestBenchmarks*
```

### Run with specific diagnoser:
```bash
dotnet run -c Release --filter *PlaceOfInterestBenchmarks* --memory
```

## Benchmark Features

All benchmarks include:
- **MemoryDiagnoser**: Tracks memory allocations and GC collections
- **CPUUsageDiagnoser**: Measures CPU usage
- **Mock repositories**: Using NSubstitute for fast, isolated tests
- **Realistic data**: Representative request objects with typical data

## Results Interpretation

The benchmarks measure:
- **Mean execution time**: Average time per operation
- **Memory allocations**: Bytes allocated per operation
- **GC collections**: Gen 0/1/2 collections
- **CPU usage**: Processor time consumed

## Mock Implementation

The `MockRepositoryFactory` provides:
- Consistent mock behavior across benchmarks
- Pre-configured sample data
- Non-blocking async operations
- Isolated testing environment

## Notes

- All benchmarks run in Release mode for accurate performance metrics
- Results are saved in `BenchmarkDotNet.Artifacts/results/`
- Mock repositories ensure consistent, fast execution without database overhead
- Benchmarks focus on handler and validation logic, not data access performance
