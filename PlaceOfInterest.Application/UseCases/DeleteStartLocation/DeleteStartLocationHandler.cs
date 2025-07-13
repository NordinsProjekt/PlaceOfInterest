using FluentValidation;
using MediatR;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.DeleteStartLocation;

public class DeleteStartLocationHandler(DeleteStartLocationValidator validator, IRepository<StartLocation> repository)
    : IRequestHandler<DeleteStartLocationRequest, bool>
{
    public async Task<bool> Handle(DeleteStartLocationRequest request, CancellationToken cancellationToken)
    {
        request.PreProcess(repository);
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            throw new ValidationException(GetType().Name, validatorResult.Errors);

        var startLocationEntity = await repository.GetByIdAsync(request.Id);

        await repository.DeleteAsync(startLocationEntity);
        await repository.SaveChangesAsync();

        return true;
    }
}
