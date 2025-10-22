using BenchmarkDotNet.Attributes;
using Microsoft.VSDiagnostics;
using PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;
using PlaceOfInterest.Application.UseCases.UpdatePlaceOfInterest;
using PlaceOfInterest.Application.UseCases.DeletePlaceOfInterest;
using BenchmarkApplicationTests.Mocks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BenchmarkApplicationTests;

/// <summary>
/// Benchmarks for PlaceOfInterest use cases (Create, Update, Delete)
/// </summary>
[MemoryDiagnoser]
[CPUUsageDiagnoser]
public class PlaceOfInterestBenchmarks
{
    private CreatePlaceOfInterestHandler _createHandler = null!;
    private UpdatePlaceOfInterestHandler _updateHandler = null!;
    private DeletePlaceOfInterestHandler _deleteHandler = null!;
    
    private CreatePlaceOfInterestRequest _createRequest = null!;
    private UpdatePlaceOfInterestRequest _updateRequest = null!;
    private DeletePlaceOfInterestRequest _deleteRequest = null!;

    [GlobalSetup]
    public void Setup()
    {
        var poiRepository = MockRepositoryFactory.CreatePlaceOfInterestRepository();
        var startRepository = MockRepositoryFactory.CreateStartLocationRepository();
        var endRepository = MockRepositoryFactory.CreateEndLocationRepository();
        
        var createValidator = new CreatePlaceOfInterestValidator();
        var updateValidator = new UpdatePlaceOfInterestValidator();
        var deleteValidator = new DeletePlaceOfInterestValidator();

        _createHandler = new CreatePlaceOfInterestHandler(
            createValidator,
            poiRepository,
            startRepository,
            endRepository);

        _updateHandler = new UpdatePlaceOfInterestHandler(
            updateValidator,
            poiRepository,
            startRepository,
            endRepository);

        _deleteHandler = new DeletePlaceOfInterestHandler(
            poiRepository,
            deleteValidator);

        _createRequest = new CreatePlaceOfInterestRequest
        {
            Name = "Benchmark Place",
            Description = "Test description for benchmarking",
            StartLocation = "40.7128,-74.0060",
            EndLocation = "34.0522,-118.2437",
            TerrainScore = 3,
            ImageUrl = "https://example.com/image.jpg"
        };

        _updateRequest = new UpdatePlaceOfInterestRequest
        {
            Id = Guid.NewGuid(),
            Name = "Updated Place",
            Description = "Updated description"
        };

        _deleteRequest = new DeletePlaceOfInterestRequest
        {
            Id = Guid.NewGuid()
        };
    }

    [Benchmark]
    public async Task<string> CreatePlaceOfInterest()
    {
        return await _createHandler.Handle(_createRequest, CancellationToken.None);
    }

    [Benchmark]
    public async Task<bool> UpdatePlaceOfInterest()
    {
        return await _updateHandler.Handle(_updateRequest, CancellationToken.None);
    }

    [Benchmark]
    public async Task<bool> DeletePlaceOfInterest()
    {
        return await _deleteHandler.Handle(_deleteRequest, CancellationToken.None);
    }
}
