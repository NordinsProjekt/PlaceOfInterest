namespace PlaceOfInterest.BackOffice.Components.Pages.PlaceOfInterestPage.Models.Extensions;

public static class PlaceOfInterestExtensions
{
    public static PlaceOfInterestListItemDto ToListItemDto(this Domain.PlaceOfInterest entity)
    {
        return new PlaceOfInterestListItemDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            StartLocation = entity.StartLocation,
            EndLocation = entity.EndLocation,
            TerrainScore = entity.TerrainScore,
            Score = entity.Score,
            Verified = entity.Verified
        };
    }
}
