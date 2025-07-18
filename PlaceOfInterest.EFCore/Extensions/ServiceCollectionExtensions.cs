using Microsoft.Extensions.DependencyInjection;
using PlaceOfInterest.Application.Interfaces;

namespace PlaceOfInterest.EFCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<IStartLocationRepository, StartLocationRepository>();
        service.AddScoped<IEndLocationRepository, EndLocationRepository>();

        return service;
    }
}