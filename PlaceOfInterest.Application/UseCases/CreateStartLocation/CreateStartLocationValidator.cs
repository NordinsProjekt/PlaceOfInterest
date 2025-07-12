using FluentValidation;

namespace PlaceOfInterest.Application.UseCases.CreateStartLocation;

public class CreateStartLocationValidator : AbstractValidator<CreateStartLocationRequest>
{
    public CreateStartLocationValidator()
    {
        RuleFor(x => x.Location).NotEmpty();
    }
}
