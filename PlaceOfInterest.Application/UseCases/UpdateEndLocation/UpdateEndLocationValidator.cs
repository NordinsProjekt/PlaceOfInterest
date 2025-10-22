using FluentValidation;

namespace PlaceOfInterest.Application.UseCases.UpdateEndLocation;

public class UpdateEndLocationValidator : AbstractValidator<UpdateEndLocationRequest>
{
    public UpdateEndLocationValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Location).NotEmpty();
    }
}