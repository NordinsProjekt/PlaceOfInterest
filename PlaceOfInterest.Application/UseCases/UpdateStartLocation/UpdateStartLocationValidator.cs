using FluentValidation;

namespace PlaceOfInterest.Application.UseCases.UpdateStartLocation;

public class UpdateStartLocationValidator : AbstractValidator<UpdateStartLocationRequest>
{
    public UpdateStartLocationValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Location).NotEmpty();
    }
}
