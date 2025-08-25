using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientWebassembly;
using ClientWebassembly.Services;
using PlaceOfInterest.GoogleMaps;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register default HttpClient for general use
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register PlaceOfInterestApiClient with API base URL
builder.Services.AddScoped<PlaceOfInterestApiClient>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7049/") }; // Use your API URL
    return new PlaceOfInterestApiClient(httpClient);
});
    
// Register GoogleMapsService if API key is present
var apiKey = builder.Configuration["GoogleMaps:ApiKey"];
if (!string.IsNullOrEmpty(apiKey))
{
    builder.Services.AddScoped<GoogleMapsService>(sp =>
    {
        var httpClient = new HttpClient();
        return new GoogleMapsService(httpClient, apiKey);
    });
}

await builder.Build().RunAsync();   
