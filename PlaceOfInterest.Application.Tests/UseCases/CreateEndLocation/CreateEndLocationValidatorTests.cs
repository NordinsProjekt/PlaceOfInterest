using PlaceOfInterest.Application.UseCases.CreateEndLocation;
using PlaceOfInterest.Application.UseCases.CreateStartLocation;

namespace PlaceOfInterest.Application.Tests.UseCases.CreateEndLocation;

public class CreateEndLocationValidatorTests
{
    [Fact]
    public void Validator_AcceptedRequest_ShouldValidate()
    {
        var validator = new CreateEndLocationValidator();
        var request = new CreateEndLocationRequest { Location = "cords" };

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validator_LocationStringIsEmpty_ShouldNotValidate()
    {
        var validator = new CreateEndLocationValidator();
        var request = new CreateEndLocationRequest { Location = string.Empty };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateStartLocationRequest.Location));
    }
}