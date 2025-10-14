using BenchmarkDotNet.Attributes;
using Microsoft.VSDiagnostics;
using PlaceOfInterest.Application.UseCases.CreateEndLocation;
using PlaceOfInterest.Application.UseCases.UpdateEndLocation;
using PlaceOfInterest.Application.UseCases.DeleteEndLocation;
using BenchmarkApplicationTests.Mocks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BenchmarkApplicationTests;

/// <summary>
/// Benchmarks for EndLocation use cases (Create, Update, Delete)
/// </summary>
[MemoryDiagnoser]
[CPUUsageDiagnoser]
public class EndLocationBenchmarks
{
    private CreateEndLocationHandler _createHandler = null!;
    private UpdateEndLocationHandler _updateHandler = null!;
    private DeleteEndLocationHandler _deleteHandler = null!;
    
    private CreateEndLocationRequest _createRequest = null!;
    private UpdateEndLocationRequest _updateRequest = null!;
    private DeleteEndLocationRequest _deleteRequest = null!;

    [GlobalSetup]
    public void Setup()
    {
        var repository = MockRepositoryFactory.CreateEndLocationRepository();
        
        var createValidator = new CreateEndLocationValidator();
        var updateValidator = new UpdateEndLocationValidator();
        var deleteValidator = new DeleteEndLocationValidator();

        _createHandler = new CreateEndLocationHandler(repository, createValidator);
        _updateHandler = new UpdateEndLocationHandler(updateValidator, repository);
        _deleteHandler = new DeleteEndLocationHandler(deleteValidator, repository);

        _createRequest = new CreateEndLocationRequest
        {
            Name = "Benchmark End Location",
            Description = "Test description",
            City = "Chicago",
            Country = "USA",
            Location = "41.8781,-87.6298"
        };

        _updateRequest = new UpdateEndLocationRequest
        {
            Id = Guid.NewGuid(),
            Name = "Updated End Location",
            Description = "Updated description",
            City = "Miami",
            Country = "USA",
            Location = "25.7617,-80.1918"
        };

        _deleteRequest = new DeleteEndLocationRequest
        {
            Id = Guid.NewGuid()
        };
    }

    [Benchmark]
    public async Task<bool> CreateEndLocation()
    {
        return await _createHandler.Handle(_createRequest, CancellationToken.None);
    }

    [Benchmark]
    public async Task<bool> UpdateEndLocation()
    {
        return await _updateHandler.Handle(_updateRequest, CancellationToken.None);
    }

    [Benchmark]
    public async Task<bool> DeleteEndLocation()
    {
        return await _deleteHandler.Handle(_deleteRequest, CancellationToken.None);
    }
}
