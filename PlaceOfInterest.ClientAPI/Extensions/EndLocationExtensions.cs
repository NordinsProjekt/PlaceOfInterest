using Contracts.Models.Dto;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.ClientAPI.Extensions;

public static class EndLocationExtensions
{
    public static IEnumerable<EndLocationApiDto> ToApiDto(this IEnumerable<EndLocation> entities)
    {
        return entities.Select(entity => entity.ToApiDto());
    }

    public static EndLocationApiDto ToApiDto(this EndLocation entity)
    {
        return new EndLocationApiDto(
            entity.Id, entity.Name, entity.Description, entity.Location, entity.Verified);
    }
}
