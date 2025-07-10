using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.Server.Data;

namespace PlaceOfInterest.Server.Data
{
    public class PlaceOfInterestContext(DbContextOptions<PlaceOfInterestContext> options) : IdentityDbContext<AdminUser>(options)
    {
    }
}
