using FluentValidation;

namespace PlaceOfInterest.Application.UseCases.DeleteEndLocation;

public class DeleteEndLocationValidator : AbstractValidator<DeleteEndLocationRequest>
{
    public DeleteEndLocationValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Count).Equal(0);
    }
}