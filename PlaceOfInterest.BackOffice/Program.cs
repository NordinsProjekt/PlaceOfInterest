using System.Reflection;
using FluentValidation;
using MediatR;
using MediatRCore.Behaviors;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Application.UseCases.CreateStartLocation;
using PlaceOfInterest.BackOffice.Components;
using PlaceOfInterest.BackOffice.Components.Account;
using PlaceOfInterest.Domain;
using PlaceOfInterest.EFCore;
using PlaceOfInterest.EFCore.Extensions;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("PlaceOfInterest.Application")));

builder.Services.AddValidatorsFromAssemblyContaining<CreateStartLocationValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddRepositories();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<PlaceOfInterestContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PlaceOfInterestContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
builder.Services.AddRadzenComponents();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Automatically apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var placeContext = scope.ServiceProvider.GetRequiredService<PlaceOfInterestContext>();
    await placeContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();