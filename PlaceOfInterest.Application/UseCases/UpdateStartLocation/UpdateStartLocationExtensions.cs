using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.UpdateStartLocation;

public static class UpdateStartLocationExtensions
{
    public static UpdateStartLocationRequest ToUpdateRequest(this StartLocation entity)
    {
        return new UpdateStartLocationRequest
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

    public static void UpdateEntity(this StartLocation entity, UpdateStartLocationRequest request)
    {
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Location = request.Location;
        entity.City = request.City;
        entity.Country = request.Country;
        entity.Verified = request.Verified;
    }
}
