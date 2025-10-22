using Contracts.Models.Dto;

namespace PlaceOfInterest.ClientAPI.Extensions;

public static class PlaceOfInterestExtensions
{
    public static IEnumerable<PlaceOfInterestApiDto> ToApiDto(this IEnumerable<Domain.PlaceOfInterest> entities)
    {
        return entities.Select(x => x.ToApiDto());
    }

    public static PlaceOfInterestApiDto ToApiDto(this Domain.PlaceOfInterest entity)
    {
        return new PlaceOfInterestApiDto(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.StartLocation.ToApiDto(),
            entity.EndLocation.ToApiDto(),
            entity.ImageUrl,
            entity.TerrainScore,
            entity.Score,
            entity.Verified
        );
    }
}
