using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Application.Models;

namespace PlaceOfInterest.EFCore;

public class StartLocationRepository(PlaceOfInterestContext context) : IStartLocationRepository
{
    public List<DropDownNameIdDto> GetVerifiedNameIdStartLocation()
    {
        return context.StartLocations.Where(x => x.Verified)
            .Select(x => new DropDownNameIdDto { Id = x.Id, Name = x.Name }).ToList();
    }
}