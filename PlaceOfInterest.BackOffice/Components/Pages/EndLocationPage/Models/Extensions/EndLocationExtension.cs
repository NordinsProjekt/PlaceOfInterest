namespace PlaceOfInterest.BackOffice.Components.Pages.EndLocationPage.Models.Extensions;

public static class EndLocationExtension
{
    public static EndLocationListItemDto ToListItemDto(this Domain.EndLocation entity)
    {
        return new EndLocationListItemDto
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
