using System.Reflection;
using Contracts.Models.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.EFCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<PlaceOfInterestContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("PlaceOfInterest.Application")));

builder.Services.AddScoped<PlaceOfInterestService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PlaceOfInterest}/{action=Index}/{id?}");

app.Run();

public class PlaceOfInterestService(IMediator mediator)
{
    public Task<bool> CreateAsync(CreatePlaceOfInterestApiRequestDto dto) => mediator.Send(dto);
}