using PlaceOfInterest.Application.UseCases.UpdateEndLocation;

namespace PlaceOfInterest.Application.Tests.UseCases.UpdateEndLocation;

public class UpdateEndLocationValidatorTests
{
    private UpdateEndLocationRequest CreateRequest()
    {
        return new UpdateEndLocationRequest
        {
            Id = Guid.NewGuid(),
            Location = "Cords"
        };
    }

    [Fact]
    public void Validator_AcceptedRequest_ShouldValidate()
    {
        var validator = new UpdateEndLocationValidator();
        var request = CreateRequest();

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validator_LocationStringIsEmpty_ShouldNotValidate()
    {
        var validator = new UpdateEndLocationValidator();
        var request = CreateRequest();
        request.Location = string.Empty;

        var result = validator.Validate(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(UpdateEndLocationRequest.Location));
        });
    }

    [Fact]
    public void Validator_IdGuidIsEmpty_ShouldNotValidate()
    {
        var validator = new UpdateEndLocationValidator();
        var request = CreateRequest();
        request.Id = Guid.Empty;

        var result = validator.Validate(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(UpdateEndLocationRequest.Id));
        });
    }
}