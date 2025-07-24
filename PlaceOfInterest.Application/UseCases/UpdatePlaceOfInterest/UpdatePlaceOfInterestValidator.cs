using FluentValidation;

namespace PlaceOfInterest.Application.UseCases.UpdatePlaceOfInterest;

public class UpdatePlaceOfInterestValidator : AbstractValidator<UpdatePlaceOfInterestRequest>
{
    public UpdatePlaceOfInterestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.PublicUniqueToken)
            .NotEmpty();

        RuleFor(x => x.PublicUniqueToken)
            .Length(8);

        When(x => x.StartLocationId == Guid.Empty, () => { RuleFor(x => x.StartLocation).NotEmpty(); });

        When(x => x.EndLocationId == Guid.Empty, () => { RuleFor(x => x.EndLocation).NotEmpty(); });
    }
}