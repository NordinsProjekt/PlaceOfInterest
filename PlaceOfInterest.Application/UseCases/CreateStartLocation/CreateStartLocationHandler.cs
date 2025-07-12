using System.ComponentModel.DataAnnotations;
using MediatR;
using MediatR.Pipeline;

namespace PlaceOfInterest.Application.UseCases.CreateStartLocation;

public class CreateStartLocationHandler(CreateStartLocationValidator validator, )
    : IRequestHandler<CreateStartLocationRequest, bool>
{
    public async Task<bool> Handle(CreateStartLocationRequest request, CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid) throw new ValidationException("CreateStartLocationRequest");

        return true;
    }
}
