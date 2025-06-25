using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.BackOffice.Server.Components;
using PlaceOfInterest.BackOffice.Server.Components.Account;
using PlaceOfInterest.BackOffice.Server.Data;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

// ...

var builder = WebApplication.CreateBuilder(args);

// 1) Blazor server components with auth-state persistence
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddAuthenticationStateSerialization(); // persist auth state to client :contentReference[oaicite:0]{index=0}

// 2) Configure OpenID Connect with Microsoft Identity Web
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd")); // :contentReference[oaicite:1]{index=1}

// 3) Add authorization services (for [Authorize] etc.)
builder.Services.AddAuthorization();

var app = builder.Build();

// 4) Middleware for auth
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapStaticAssets();

// 5) Your Blazor app entrypoint
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();