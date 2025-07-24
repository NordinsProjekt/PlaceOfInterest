using MediatR;

namespace PlaceOfInterest.Application.UseCases.DeletePlaceOfInterest;

public class DeletePlaceOfInterestRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}