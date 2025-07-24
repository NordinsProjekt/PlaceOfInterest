using FluentValidation;

namespace PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;

public class CreatePlaceOfInterestValidator : AbstractValidator<CreatePlaceOfInterestRequest>
{
    public CreatePlaceOfInterestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.PublicUniqueToken)
            .NotEmpty();

        When(x => x.PublicUniqueToken.Length > 8, () =>
            RuleFor(x => x.PublicUniqueToken)
                .Length(36));

        When(x => x.PublicUniqueToken.Length < 9, () =>
            RuleFor(x => x.PublicUniqueToken)
                .Length(8));

        When(x => x.StartLocationId == Guid.Empty, () => { RuleFor(x => x.StartLocation).NotEmpty(); });

        When(x => x.EndLocationId == Guid.Empty, () => { RuleFor(x => x.EndLocation).NotEmpty(); });
    }
}