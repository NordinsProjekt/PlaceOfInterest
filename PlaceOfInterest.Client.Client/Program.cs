using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PlaceOfInterest.Client.Client;
using PlaceOfInterest.Client.Client.Services;
using PlaceOfInterest.GoogleMaps;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure HttpClient for the API
builder.Services.AddHttpClient<PlaceOfInterestApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

// Configure Google Maps Service
builder.Services.AddHttpClient<GoogleMapsService>()
    .ConfigureHttpClient(client => {
        // Configure default settings if needed
    });

builder.Services.AddSingleton(sp => 
    new GoogleMapsService(
        sp.GetRequiredService<IHttpClientFactory>().CreateClient(), 
        builder.Configuration["GoogleMaps:ApiKey"] ?? throw new InvalidOperationException("Google Maps API key not found")));

await builder.Build().RunAsync();