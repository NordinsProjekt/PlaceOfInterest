using MediatR;
using PlaceOfInterest.Domain;

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
    public StartLocation StartLocation { get; set; } = new();
    public EndLocation EndLocation { get; set; } = new();
}