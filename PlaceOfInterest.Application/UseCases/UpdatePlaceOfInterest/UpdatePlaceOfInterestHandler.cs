using FluentValidation;
using MediatR;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.UpdatePlaceOfInterest;

public class UpdatePlaceOfInterestHandler(
    UpdatePlaceOfInterestValidator validator,
    IRepository<Domain.PlaceOfInterest> repository,
    IRepository<StartLocation> startRepository,
    IRepository<EndLocation> endRepository)
    : IRequestHandler<UpdatePlaceOfInterestRequest, bool>
{
    public async Task<bool> Handle(UpdatePlaceOfInterestRequest request, CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            throw new ValidationException(GetType().Name, validatorResult.Errors);

        var placeOfInterest = await repository.GetById(request.Id, e => e.StartLocation, e => e.EndLocation);

        await placeOfInterest.UpdateEntity(request, startRepository, endRepository);

        await repository.UpdateAsync(placeOfInterest);
        await repository.SaveChangesAsync();

        return true;
    }
}