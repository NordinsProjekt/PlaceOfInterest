using MediatR;

namespace PlaceOfInterest.Application.UseCases.UpdateStartLocation;

public class UpdateStartLocationRequest : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public string Location { get; set; } = "";
    public bool Verified { get; set; }
}