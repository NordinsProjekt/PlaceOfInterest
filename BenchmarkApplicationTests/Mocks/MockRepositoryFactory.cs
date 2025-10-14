using NSubstitute;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BenchmarkApplicationTests.Mocks;

public static class MockRepositoryFactory
{
    public static IRepository<T> CreateMockRepository<T>() where T : class
    {
        var repository = Substitute.For<IRepository<T>>();
        
        // Setup common mock behaviors
        repository.AddAsync(Arg.Any<T>()).Returns(Task.CompletedTask);
        repository.SaveChangesAsync().Returns(Task.CompletedTask);
        repository.GetQuery().Returns(new List<T>().AsQueryable());
        repository.CountMatches(Arg.Any<Expression<Func<T, bool>>>()).Returns(0);
        
        return repository;
    }

    public static IRepository<PlaceOfInterest.Domain.PlaceOfInterest> CreatePlaceOfInterestRepository()
    {
        var repository = CreateMockRepository<PlaceOfInterest.Domain.PlaceOfInterest>();
        
        var mockData = new PlaceOfInterest.Domain.PlaceOfInterest
        {
            Id = Guid.NewGuid(),
            Name = "Test Place",
            Description = "Test Description",
            StartLocation = new StartLocation { Id = Guid.NewGuid(), Name = "Start" },
            EndLocation = new EndLocation { Id = Guid.NewGuid(), Name = "End" }
        };
        
        repository.GetById(Arg.Any<Guid>(), Arg.Any<Expression<Func<PlaceOfInterest.Domain.PlaceOfInterest, object>>[]>())
            .Returns(mockData);
        
        return repository;
    }

    public static IRepository<StartLocation> CreateStartLocationRepository()
    {
        var repository = CreateMockRepository<StartLocation>();
        
        var mockData = new StartLocation
        {
            Id = Guid.NewGuid(),
            Name = "Test Start Location",
            Location = "40.7128,-74.0060"
        };
        
        repository.GetById(Arg.Any<Guid>(), Arg.Any<Expression<Func<StartLocation, object>>[]>())
            .Returns(mockData);
        
        return repository;
    }

    public static IRepository<EndLocation> CreateEndLocationRepository()
    {
        var repository = CreateMockRepository<EndLocation>();
        
        var mockData = new EndLocation
        {
            Id = Guid.NewGuid(),
            Name = "Test End Location",
            Location = "34.0522,-118.2437"
        };
        
        repository.GetById(Arg.Any<Guid>(), Arg.Any<Expression<Func<EndLocation, object>>[]>())
            .Returns(mockData);
        
        return repository;
    }
}
