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

        //RuleFor(x => x.StartLocation)
        //    .ChildRules(startLocation => { startLocation.RuleFor(sl => sl.Location).NotEmpty(); });

        //RuleFor(x => x.EndLocation)
        //    .ChildRules(endLocation => { endLocation.RuleFor(el => el.Location).NotEmpty(); });
    }
}