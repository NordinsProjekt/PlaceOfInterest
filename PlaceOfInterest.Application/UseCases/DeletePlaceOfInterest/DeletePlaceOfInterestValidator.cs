using FluentValidation;

namespace PlaceOfInterest.Application.UseCases.DeletePlaceOfInterest;

public class DeletePlaceOfInterestValidator : AbstractValidator<DeletePlaceOfInterestRequest>
{
    public DeletePlaceOfInterestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
