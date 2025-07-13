using PlaceOfInterest.Application.UseCases.CreateStartLocation;

namespace PlaceOfInterest.Application.Tests.UseCases.CreateStartLocation;

public class CreateStartLocationValidatorTests
{
    [Fact]
    public void Validator_AcceptedRequest_ShouldValidate()
    {
        var validator = new CreateStartLocationValidator();
        var request = new CreateStartLocationRequest { Location = "cords" };

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validator_LocationStringIsEmpty_ShouldNotValidate()
    {
        var validator = new CreateStartLocationValidator();
        var request = new CreateStartLocationRequest { Location = string.Empty };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateStartLocationRequest.Location));
    }
}
