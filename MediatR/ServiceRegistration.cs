using Microsoft.Extensions.DependencyInjection;
using PlaceOfInterest.Application.Interfaces;

namespace MediatRCore;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRequest<>), typeof(MediatR.IRequest<>));
        services.AddTransient(typeof(IRequestHandler<,>),
            typeof(MediatR.IRequestHandler<,>));
        return services;
    }
}
