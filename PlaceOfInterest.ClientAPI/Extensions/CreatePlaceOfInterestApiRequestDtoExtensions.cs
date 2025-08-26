using Contracts.Models.Requests;
using PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;

namespace PlaceOfInterest.ClientAPI.Extensions;

public static class CreatePlaceOfInterestApiRequestDtoExtensions
{
    public static CreatePlaceOfInterestRequest ToRequest(this CreatePlaceOfInterestApiRequestDto dto)
    {
        return new CreatePlaceOfInterestRequest
        {
            Name = dto.Name,
            Description = dto.Description,
            StartLocation = dto.StartLocation,
            EndLocation = dto.EndLocation,
            ImageUrl = dto.ImageUrl
        };
    }
}
