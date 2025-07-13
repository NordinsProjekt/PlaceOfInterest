using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.CreateEndLocation;

public static class CreateEndLocationExtension
{
    public static EndLocation ToDbEntity(this CreateEndLocationRequest request)
    {
        return new EndLocation
        {
            Created = DateTime.UtcNow,
            Name = request.Name,
            Description = request.Description,
            Location = request.Location,
            City = request.City,
            Country = request.Country,
            Verified = false
        };
    }
}
