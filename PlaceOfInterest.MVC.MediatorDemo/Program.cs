using MediatR;
using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;
using PlaceOfInterest.EFCore;
using System.Reflection;
using ContractsReq = Contracts.Models.Requests;

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
    public Task<bool> CreateAsync(ContractsReq.CreatePlaceOfInterestApiRequestDto dto)
    {
        var req = new CreatePlaceOfInterestRequest
        {
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            TerrainScore = (byte)dto.TerrainScore,
            StartLocation = dto.StartLocation,
            EndLocation = dto.EndLocation
        };
        return mediator.Send(req);
    }
}