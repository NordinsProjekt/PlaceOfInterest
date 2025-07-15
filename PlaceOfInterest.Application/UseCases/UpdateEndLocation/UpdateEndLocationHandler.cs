using FluentValidation;
using MediatR;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.UpdateEndLocation;

public class UpdateEndLocationHandler(UpdateEndLocationValidator validator, IRepository<EndLocation> repository)
    : IRequestHandler<UpdateEndLocationRequest, bool>
{
    public async Task<bool> Handle(UpdateEndLocationRequest request, CancellationToken cancellationToken)
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