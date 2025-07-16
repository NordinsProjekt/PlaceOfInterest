namespace PlaceOfInterest.BackOffice.Components.Pages.PlaceOfInterest.Models;

public class PlaceOfInterestListItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public Domain.StartLocation StartLocation { get; set; }
    public Domain.EndLocation EndLocation { get; set; }
    public byte TerrainScore { get; set; } //1-5
    public byte Score { get; set; } //1-5 stars
    public bool Verified { get; set; }
}
