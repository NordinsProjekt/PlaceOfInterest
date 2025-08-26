using Contracts.Models.Dto;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.ClientAPI.Extensions;

public static class StartLocationExtensions
{
    public static IEnumerable<StartLocationApiDto> ToApiDto(this IEnumerable<StartLocation> entities)
    {
        return entities.Select(entity => entity.ToApiDto());
    }

    public static StartLocationApiDto ToApiDto(this StartLocation entity)
    {
        return new StartLocationApiDto(
            entity.Id, entity.Name, entity.Description, entity.Location, entity.Verified);
    }
}
