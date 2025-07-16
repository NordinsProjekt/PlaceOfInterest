using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.CreateStartLocation;

public static class CreateStartLocationExtensions
{
    public static StartLocation ToDbEntity(this CreateStartLocationRequest request)
    {
        return new StartLocation
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
