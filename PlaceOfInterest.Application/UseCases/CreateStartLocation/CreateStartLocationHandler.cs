using FluentValidation;
using MediatR;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.CreateStartLocation;

public class CreateStartLocationHandler(CreateStartLocationValidator validator, IRepository<StartLocation> repository)
    : IRequestHandler<CreateStartLocationRequest, bool>
{
    public async Task<bool> Handle(CreateStartLocationRequest request, CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            throw new ValidationException(GetType().Name, validatorResult.Errors);

        var startLocationEntity = request.ToDbEntity();

        await repository.AddAsync(startLocationEntity);
        await repository.SaveChangesAsync();

        return true;
    }
}
