using MediatR;

namespace PlaceOfInterest.Application.UseCases.DeleteStartLocation;

public class DeleteStartLocationRequest : IRequest<bool>
{
    public Guid Id { get; set; }
    public int Count { get; set; }
}