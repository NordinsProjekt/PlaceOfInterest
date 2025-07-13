using FluentValidation;

namespace PlaceOfInterest.Application.UseCases.DeleteStartLocation;

public class DeleteStartLocationValidator : AbstractValidator<DeleteStartLocationRequest>
{
    public DeleteStartLocationValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Count).Equal(0);
    }
}