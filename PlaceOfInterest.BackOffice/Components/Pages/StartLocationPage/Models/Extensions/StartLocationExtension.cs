using PlaceOfInterest.BackOffice.Components.Pages.StartLocationPage.Models;

namespace PlaceOfInterest.BackOffice.Components.Pages.StartLocationPage.Models.Extensions;

public static class StartLocationExtension
{
    public static StartLocationListItemDto ToListItemDto(this Domain.StartLocation entity)
    {
        return new StartLocationListItemDto
        {
            Id = entity.Id,
            Created = entity.Created,
            Name = entity.Name,
            Description = entity.Description,
            Location = entity.Location,
            City = entity.City,
            Country = entity.Country,
            Verified = entity.Verified
        };
    }
}
