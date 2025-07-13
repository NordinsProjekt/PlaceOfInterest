using PlaceOfInterest.Application.UseCases.UpdateStartLocation;

namespace PlaceOfInterest.Application.Tests.UseCases.UpdateStartLocation;

public class UpdateStartLocationValidatorTests
{
    private UpdateStartLocationRequest CreateRequest()
    {
        return new UpdateStartLocationRequest
        {
            Id = Guid.NewGuid(),
            Location = "Cords"
        };
    }

    [Fact]
    public void Validator_AcceptedRequest_ShouldValidate()
    {
        var validator = new UpdateStartLocationValidator();
        var request = CreateRequest();

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validator_LocationStringIsEmpty_ShouldNotValidate()
    {
        var validator = new UpdateStartLocationValidator();
        var request = CreateRequest();
        request.Location = string.Empty;

        var result = validator.Validate(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(UpdateStartLocationRequest.Location));
        });
    }

    [Fact]
    public void Validator_IdGuidIsEmpty_ShouldNotValidate()
    {
        var validator = new UpdateStartLocationValidator();
        var request = CreateRequest();
        request.Id = Guid.Empty;

        var result = validator.Validate(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(UpdateStartLocationRequest.Id));
        });
    }
}