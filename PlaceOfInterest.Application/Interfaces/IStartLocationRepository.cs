using PlaceOfInterest.Application.Models;

namespace PlaceOfInterest.Application.Interfaces;

public interface IStartLocationRepository
{
    List<DropDownNameIdDto> GetVerifiedNameIdStartLocation();
}