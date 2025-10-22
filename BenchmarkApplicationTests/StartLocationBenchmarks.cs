using BenchmarkDotNet.Attributes;
using Microsoft.VSDiagnostics;
using PlaceOfInterest.Application.UseCases.CreateStartLocation;
using PlaceOfInterest.Application.UseCases.UpdateStartLocation;
using PlaceOfInterest.Application.UseCases.DeleteStartLocation;
using BenchmarkApplicationTests.Mocks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BenchmarkApplicationTests;

/// <summary>
/// Benchmarks for StartLocation use cases (Create, Update, Delete)
/// </summary>
[MemoryDiagnoser]
[CPUUsageDiagnoser]
public class StartLocationBenchmarks
{
    private CreateStartLocationHandler _createHandler = null!;
    private UpdateStartLocationHandler _updateHandler = null!;
    private DeleteStartLocationHandler _deleteHandler = null!;
    
    private CreateStartLocationRequest _createRequest = null!;
    private UpdateStartLocationRequest _updateRequest = null!;
    private DeleteStartLocationRequest _deleteRequest = null!;

    [GlobalSetup]
    public void Setup()
    {
        var repository = MockRepositoryFactory.CreateStartLocationRepository();
        
        var createValidator = new CreateStartLocationValidator();
        var updateValidator = new UpdateStartLocationValidator();
        var deleteValidator = new DeleteStartLocationValidator();

        _createHandler = new CreateStartLocationHandler(createValidator, repository);
        _updateHandler = new UpdateStartLocationHandler(updateValidator, repository);
        _deleteHandler = new DeleteStartLocationHandler(deleteValidator, repository);

        _createRequest = new CreateStartLocationRequest
        {
            Name = "Benchmark Start Location",
            Description = "Test description",
            City = "New York",
            Country = "USA",
            Location = "40.7128,-74.0060"
        };

        _updateRequest = new UpdateStartLocationRequest
        {
            Id = Guid.NewGuid(),
            Name = "Updated Start Location",
            Description = "Updated description",
            City = "Los Angeles",
            Country = "USA",
            Location = "34.0522,-118.2437"
        };

        _deleteRequest = new DeleteStartLocationRequest
        {
            Id = Guid.NewGuid()
        };
    }

    [Benchmark]
    public async Task<bool> CreateStartLocation()
    {
        return await _createHandler.Handle(_createRequest, CancellationToken.None);
    }

    [Benchmark]
    public async Task<bool> UpdateStartLocation()
    {
        return await _updateHandler.Handle(_updateRequest, CancellationToken.None);
    }

    [Benchmark]
    public async Task<bool> DeleteStartLocation()
    {
        return await _deleteHandler.Handle(_deleteRequest, CancellationToken.None);
    }
}
