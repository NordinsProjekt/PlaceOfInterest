using PlaceOfInterest.Application.UseCases.DeletePlaceOfInterest;

namespace PlaceOfInterest.Application.Tests.UseCases.DeletePlaceOfInterest;

public class DeletePlaceOfInterestValidatorTests
{
    [Fact]
    public void Validate_RequestIsValid_ShouldValidate()
    {
        var validator = new DeletePlaceOfInterestValidator();
        var request = new DeletePlaceOfInterestRequest { Id = Guid.NewGuid() };

        var result = validator.Validate(request);

        Assert.Multiple(() =>
        {
            Assert.True(result.IsValid);
            Assert.True(result.Errors.Count == 0);
        });
    }

    [Fact]
    public void Validate_RequestIdIsEmpty_ShouldNotValidate()
    {
        var validator = new DeletePlaceOfInterestValidator();
        var request = new DeletePlaceOfInterestRequest { Id = Guid.Empty };

        var result = validator.Validate(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(DeletePlaceOfInterestRequest.Id));
        });
    }
}
