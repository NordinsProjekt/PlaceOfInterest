using FluentValidation;

namespace PlaceOfInterest.Application.UseCases.CreateEndLocation;

public class CreateEndLocationValidator : AbstractValidator<CreateEndLocationRequest>
{
    public CreateEndLocationValidator()
    {
        RuleFor(request => request.Location)
            .NotEmpty();
    }
}