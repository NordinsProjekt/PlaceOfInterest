using PlaceOfInterest.Application.Models;

namespace PlaceOfInterest.Application.Interfaces;

public interface IEndLocationRepository
{
    List<DropDownNameIdDto> GetVerifiedNameIdEndLocation();
}