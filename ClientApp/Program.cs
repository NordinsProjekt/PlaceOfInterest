using ClientApp;
using ClientApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add default HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register PlaceOfInterestApiClient
builder.Services.AddScoped<PlaceOfInterestApiClient>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
    return new PlaceOfInterestApiClient(httpClient);
});

// Register GoogleMapsService
builder.Services.AddScoped<GoogleMapsService>(sp =>
{
    var httpClient = new HttpClient();
    var apiKey = builder.Configuration["GoogleMaps:ApiKey"]
                 ?? throw new InvalidOperationException("Google Maps API key not found");
    return new GoogleMapsService(httpClient, apiKey);
});

await builder.Build().RunAsync();
