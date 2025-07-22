using FluentValidation;
using MediatR;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;

public class CreatePlaceOfInterestHandler(
    CreatePlaceOfInterestValidator validator,
    IRepository<Domain.PlaceOfInterest> repository,
    IRepository<StartLocation> startRepository,
    IRepository<EndLocation> endRepository)
    : IRequestHandler<CreatePlaceOfInterestRequest, bool>
{
    public async Task<bool> Handle(CreatePlaceOfInterestRequest request, CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            throw new ValidationException(GetType().Name, validatorResult.Errors);

        request.PreProcess(repository);

        var placeOfInterest = await request.ToDbEntity(startRepository, endRepository);

        await repository.AddAsync(placeOfInterest);
        await repository.SaveChangesAsync();

        return true;
    }
}
