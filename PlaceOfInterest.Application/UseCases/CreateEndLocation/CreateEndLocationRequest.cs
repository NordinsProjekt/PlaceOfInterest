using MediatR;

namespace PlaceOfInterest.Application.UseCases.CreateEndLocation;

public class CreateEndLocationRequest : IRequest<bool>
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
}