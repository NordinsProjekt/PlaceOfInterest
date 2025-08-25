using MediatR;
using PlaceOfInterest.Application.Interfaces;
using ValidationException = FluentValidation.ValidationException;

namespace PlaceOfInterest.Application.UseCases.DeletePlaceOfInterest;

public class DeletePlaceOfInterestHandler(
    IRepository<Domain.PlaceOfInterest> repository,
    DeletePlaceOfInterestValidator validator)
    : IRequestHandler<DeletePlaceOfInterestRequest, bool>
{
    public async Task<bool> Handle(DeletePlaceOfInterestRequest request, CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            throw new ValidationException(GetType().Name, validatorResult.Errors);

        var placeOfInterest = await repository.GetById(request.Id);

        await repository.DeleteAsync(placeOfInterest);
        await repository.SaveChangesAsync();

        return true;
    }
}