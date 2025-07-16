using PlaceOfInterest.Domain.Interface;

namespace PlaceOfInterest.Domain;

public class PlaceOfInterest : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string PublicUniqueToken { get; set; } = "";
    public DateTime Created { get; set; }
    public required StartLocation StartLocation { get; set; }
    public required EndLocation EndLocation { get; set; }
    public byte[] ImageBytes { get; set; } = [];
    public string ImageType { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public byte TerrainScore { get; set; }
    public byte Score { get; set; } //1-5 stars
    public bool Verified { get; set; }
}