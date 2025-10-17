using ClientWebassembly;
using ClientWebassembly.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<PlaceOfInterestApiClient>(sp =>
{
    var apiBaseUrl = "https://placeofinterestclientapi20251014065818-d9f8bddyfubndzhu.northeurope-01.azurewebsites.net";

    var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    return new PlaceOfInterestApiClient(httpClient);
});

await builder.Build().RunAsync();
