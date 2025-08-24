using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PlaceOfInterest.Client.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

// Add default HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add HttpClient Factory support


// Register PlaceOfInterestApiClient
builder.Services.AddScoped<PlaceOfInterestApiClient>(sp =>
{
    // Create a new HttpClient instance for the API
    var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
    return new PlaceOfInterestApiClient(httpClient);
});

// Register GoogleMapsService
builder.Services.AddScoped<GoogleMapsService>(sp =>
{
    // Create a new HttpClient instance for Google Maps
    var httpClient = new HttpClient();
    var apiKey = builder.Configuration["GoogleMaps:ApiKey"]
                 ?? throw new InvalidOperationException("Google Maps API key not found");
    return new GoogleMapsService(httpClient, apiKey);
});

await builder.Build().RunAsync();