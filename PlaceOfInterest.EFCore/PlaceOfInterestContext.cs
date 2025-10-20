using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.EFCore;

public class PlaceOfInterestContext(DbContextOptions<PlaceOfInterestContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Domain.PlaceOfInterest> PlaceOfInterests { get; set; }
    public DbSet<StartLocation> StartLocations { get; set; }
    public DbSet<EndLocation> EndLocations { get; set; }
}