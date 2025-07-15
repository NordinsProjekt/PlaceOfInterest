using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.UpdateEndLocation;

public static class UpdateEndLocationExtension
{
    public static UpdateEndLocationRequest ToUpdateRequest(this EndLocation entity)
    {
        return new UpdateEndLocationRequest
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Location = entity.Location,
            City = entity.City,
            Country = entity.Country,
            Verified = entity.Verified
        };
    }

    public static void UpdateEntity(this EndLocation entity, UpdateEndLocationRequest request)
    {
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Location = request.Location;
        entity.City = request.City;
        entity.Country = request.Country;
        entity.Verified = request.Verified;
    }
}
