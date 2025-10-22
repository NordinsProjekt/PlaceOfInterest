using System.Reflection;
using FluentValidation;
using MediatR;
using MediatRCore.Behaviors;
using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;
using PlaceOfInterest.EFCore;
using PlaceOfInterest.EFCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // Development: Allow localhost origins for Blazor client with fixed ports
            policy.WithOrigins(
                    "https://localhost:7240", // Blazor WASM HTTPS
                    "http://localhost:5176") // Blazor WASM HTTP
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
        else
        {
            // Production: Get allowed origins from configuration or use defaults
            var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? new[]
            {
                "https://clientwebassembly20251014073000-df6mneahram.northeurope-01.azurewebsites.net",
                "https://placeofinterestclient-f7dfdcfrckgczxcdp.northeurope-01.azurewebsites.net"
            };

            policy.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("*")
                .SetIsOriginAllowed(origin =>
                {
                    // Allow any Azure websites domain for this app
                    return origin.Contains("azurewebsites.net") &&
                           (origin.Contains("clientwebassembly") ||
                            origin.Contains("placeofinterestclient"));
                });
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

Console.WriteLine($"Using connection string: {connectionString.Substring(0, Math.Min(50, connectionString.Length))}...");

builder.Services.AddDbContext<PlaceOfInterestContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("PlaceOfInterest.Application")));

builder.Services.AddValidatorsFromAssemblyContaining<CreatePlaceOfInterestValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Add middleware to log CORS issues
    app.Use(async (context, next) =>
    {
        var origin = context.Request.Headers["Origin"].FirstOrDefault();
        if (!string.IsNullOrEmpty(origin)) Console.WriteLine($"Request from origin: {origin}");

        await next();

        if (context.Response.StatusCode == 200)
            Console.WriteLine(
                $"Response headers: {string.Join(", ", context.Response.Headers.Select(h => $"{h.Key}:{h.Value}"))}");
    });
}

app.UseCors("AllowBlazorClient");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
