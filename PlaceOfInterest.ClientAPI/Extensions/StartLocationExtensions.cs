using PlaceOfInterest.ClientAPI.Dtos;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.ClientAPI.Extensions;

public static class StartLocationExtensions
{
    public static IEnumerable<StartLocationApiDto> ToApiDto(this IEnumerable<StartLocation> entities)
    {
        return entities.Select(entity => new StartLocationApiDto(
            entity.Id, entity.Name, entity.Description, entity.Location, entity.Verified));
    }
}
