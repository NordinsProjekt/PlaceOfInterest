using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.Server.Data;

namespace PlaceOfInterest.Server.Data
{
    public class PlaceOfInterestServerContext(DbContextOptions<PlaceOfInterestServerContext> options) : IdentityDbContext<PlaceOfInterestServerUser>(options)
    {
    }
}
