using MediatR;

namespace PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;

public class CreatePlaceOfInterestRequest : IRequest<bool>
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string PublicUniqueToken { get; set; } = "";
    public byte[] ImageBytes { get; set; } = [];
    public string ImageType { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public byte TerrainScore { get; set; }
    public Guid StartLocationId { get; set; }
    public string StartLocation { get; set; } = "";
    public Guid EndLocationId { get; set; }
    public string EndLocation { get; set; } = "";
}