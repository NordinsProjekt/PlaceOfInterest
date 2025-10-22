using ClientWebassembly;
using ClientWebassembly.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<PlaceOfInterestApiClient>(sp =>
{
    var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"]
          ?? throw new InvalidOperationException("API base URL not found in configuration");

    var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    return new PlaceOfInterestApiClient(httpClient);
});

await builder.Build().RunAsync();
