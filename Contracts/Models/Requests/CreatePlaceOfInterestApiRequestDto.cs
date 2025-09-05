namespace Contracts.Models.Requests;

public class CreatePlaceOfInterestApiRequestDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string StartLocation { get; set; } = "";
    public string EndLocation { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public int TerrainScore { get; set; }
}