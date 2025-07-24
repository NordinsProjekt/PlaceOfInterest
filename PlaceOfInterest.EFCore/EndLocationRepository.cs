using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Application.Models;

namespace PlaceOfInterest.EFCore;

public class EndLocationRepository(PlaceOfInterestContext context) : IEndLocationRepository
{
    public List<DropDownNameIdDto> GetVerifiedNameIdEndLocation()
    {
        return context.EndLocations.Where(e => e.Verified)
            .Select(e => new DropDownNameIdDto { Id = e.Id, Name = e.Name })
            .ToList();
    }
}