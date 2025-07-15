using FluentValidation;
using MediatR;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.UpdateStartLocation;

public class UpdateStartLocationHandler(UpdateStartLocationValidator validator, IRepository<StartLocation> repository)
    : IRequestHandler<UpdateStartLocationRequest, bool>
{
    public async Task<bool> Handle(UpdateStartLocationRequest request, CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            throw new ValidationException(GetType().Name, validatorResult.Errors);

        var startLocationEntity = await repository.GetByIdAsync(request.Id);
        startLocationEntity.UpdateEntity(request);

        await repository.UpdateAsync(startLocationEntity);
        await repository.SaveChangesAsync();

        return true;
    }
}
