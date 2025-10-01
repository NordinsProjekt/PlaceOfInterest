using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PlaceOfInterest.Client.Components;
using PlaceOfInterest.Client.Services;
using PlaceOfInterest.GoogleMaps;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<PlaceOfInterestApiClient>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
    return new PlaceOfInterestApiClient(httpClient);
});

builder.Services.AddScoped<GoogleMapsService>(sp =>
{
    var httpClient = new HttpClient();
    var apiKey = builder.Configuration["GoogleMaps:ApiKey"]
                 ?? throw new InvalidOperationException("Google Maps API key not found");
    return new GoogleMapsService(httpClient, apiKey);
});

await builder.Build().RunAsync();
