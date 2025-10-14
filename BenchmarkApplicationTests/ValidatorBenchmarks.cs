using BenchmarkDotNet.Attributes;
using Microsoft.VSDiagnostics;
using PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;
using PlaceOfInterest.Application.UseCases.CreateStartLocation;
using PlaceOfInterest.Application.UseCases.CreateEndLocation;
using BenchmarkApplicationTests.Mocks;
using System.Threading.Tasks;

namespace BenchmarkApplicationTests;

/// <summary>
/// Benchmarks comparing validation performance across different validators
/// </summary>
[MemoryDiagnoser]
[CPUUsageDiagnoser]
public class ValidatorBenchmarks
{
    private CreatePlaceOfInterestValidator _poiValidator = null!;
    private CreateStartLocationValidator _startValidator = null!;
    private CreateEndLocationValidator _endValidator = null!;
    
    private CreatePlaceOfInterestRequest _poiRequest = null!;
    private CreateStartLocationRequest _startRequest = null!;
    private CreateEndLocationRequest _endRequest = null!;

    [GlobalSetup]
    public void Setup()
    {
        _poiValidator = new CreatePlaceOfInterestValidator();
        _startValidator = new CreateStartLocationValidator();
        _endValidator = new CreateEndLocationValidator();

        _poiRequest = new CreatePlaceOfInterestRequest
        {
            Name = "Test Place",
            Description = "Description",
            StartLocation = "40.7128,-74.0060",
            EndLocation = "34.0522,-118.2437",
            TerrainScore = 3
        };

        _startRequest = new CreateStartLocationRequest
        {
            Name = "Test Start",
            Description = "Description",
            City = "New York",
            Country = "USA",
            Location = "40.7128,-74.0060"
        };

        _endRequest = new CreateEndLocationRequest
        {
            Name = "Test End",
            Description = "Description",
            City = "Los Angeles",
            Country = "USA",
            Location = "34.0522,-118.2437"
        };
    }

    [Benchmark]
    public async Task ValidatePlaceOfInterestRequest()
    {
        await _poiValidator.ValidateAsync(_poiRequest);
    }

    [Benchmark]
    public async Task ValidateStartLocationRequest()
    {
        await _startValidator.ValidateAsync(_startRequest);
    }

    [Benchmark]
    public async Task ValidateEndLocationRequest()
    {
        await _endValidator.ValidateAsync(_endRequest);
    }
}
