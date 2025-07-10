using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.EFCore;

public class PlaceOfInterestContext(DbContextOptions<PlaceOfInterestContext> options) : DbContext(options)
{
    public DbSet<Domain.PlaceOfInterest> PlaceOfInterests { get; set; }
    public DbSet<StartLocation> StartLocations { get; set; }
    public DbSet<EndLocation> EndLocations { get; set; }
}