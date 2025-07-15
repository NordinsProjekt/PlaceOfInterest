using MediatR;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;
using ValidationException = FluentValidation.ValidationException;

namespace PlaceOfInterest.Application.UseCases.CreateEndLocation;

public class CreateEndLocationHandler(IRepository<EndLocation> repository, CreateEndLocationValidator validator)
    : IRequestHandler<CreateEndLocationRequest, bool>
{
    public async Task<bool> Handle(CreateEndLocationRequest request, CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            throw new ValidationException(GetType().Name, validatorResult.Errors);

        var endLocationEntity = request.ToDbEntity();

        await repository.AddAsync(endLocationEntity);
        await repository.SaveChangesAsync();

        return true;
    }
}