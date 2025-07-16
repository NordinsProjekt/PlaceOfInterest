using FluentValidation;
using MediatR;
using PlaceOfInterest.Application.Interfaces;

namespace PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;

public class CreatePlaceOfInterestHandler(
    CreatePlaceOfInterestValidator validator,
    IRepository<Domain.PlaceOfInterest> repository)
    : IRequestHandler<CreatePlaceOfInterestRequest, bool>
{
    public async Task<bool> Handle(CreatePlaceOfInterestRequest request, CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            throw new ValidationException(GetType().Name, validatorResult.Errors);

        request.PreProcess(repository);

        var placeOfInterest = request.ToDbEntity();

        //await repository.AddAsync(startLocationEntity);
        //await repository.SaveChangesAsync();

        return true;
    }
}
