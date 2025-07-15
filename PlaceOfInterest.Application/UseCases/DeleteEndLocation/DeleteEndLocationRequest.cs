using MediatR;

namespace PlaceOfInterest.Application.UseCases.DeleteEndLocation;

public class DeleteEndLocationRequest : IRequest<bool>
{
    public Guid Id { get; set; }
    public int Count { get; set; }
}