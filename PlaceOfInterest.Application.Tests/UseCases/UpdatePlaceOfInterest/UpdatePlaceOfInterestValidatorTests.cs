using PlaceOfInterest.Application.UseCases.UpdatePlaceOfInterest;

namespace PlaceOfInterest.Application.Tests.UseCases.UpdatePlaceOfInterest;

public class UpdatePlaceOfInterestValidatorTests
{
    [Fact]
    public void Validator_AcceptedRequest_ShouldValidate()
    {
        var validator = new UpdatePlaceOfInterestValidator();
        var request = new UpdatePlaceOfInterestRequest
        {
            StartLocation = "start",
            EndLocation = "end",
            Name = "name",
            PublicUniqueToken = "MustBe8c"
        };

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validator_NameStringIsEmpty_ShouldNotValidate()
    {
        var validator = new UpdatePlaceOfInterestValidator();
        var request = new UpdatePlaceOfInterestRequest
        {
            Name = ""
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(UpdatePlaceOfInterestRequest.Name));
    }

    [Fact]
    public void Validator_NameStringIsToLong_ShouldNotValidate()
    {
        var validator = new UpdatePlaceOfInterestValidator();
        var request = new UpdatePlaceOfInterestRequest
        {
            Name = new string('a', 101)
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(UpdatePlaceOfInterestRequest.Name));
    }

    [Fact]
    public void Validator_StartLocationIsEmpty_ShouldNotValidate()
    {
        var validator = new UpdatePlaceOfInterestValidator();
        var request = new UpdatePlaceOfInterestRequest
        {
            StartLocation = ""
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors,
            error => error.PropertyName == nameof(UpdatePlaceOfInterestRequest.StartLocation));
    }

    [Fact]
    public void Validator_EndLocationIsEmpty_ShouldNotValidate()
    {
        var validator = new UpdatePlaceOfInterestValidator();
        var request = new UpdatePlaceOfInterestRequest
        {
            EndLocation = ""
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(UpdatePlaceOfInterestRequest.EndLocation));
    }

    [Fact]
    public void Validator_PublicUniqueTokenIsEmpty_ShouldNotValidate()
    {
        var validator = new UpdatePlaceOfInterestValidator();
        var request = new UpdatePlaceOfInterestRequest
        {
            PublicUniqueToken = ""
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(UpdatePlaceOfInterestRequest.EndLocation));
    }

    [Fact]
    public void Validator_PublicUniqueTokenIsToShort_ShouldNotValidate()
    {
        var validator = new UpdatePlaceOfInterestValidator();
        var request = new UpdatePlaceOfInterestRequest
        {
            PublicUniqueToken = "Short"
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(UpdatePlaceOfInterestRequest.EndLocation));
    }

    [Fact]
    public void Validator_PublicUniqueTokenIsToLong_ShouldNotValidate()
    {
        var validator = new UpdatePlaceOfInterestValidator();
        var request = new UpdatePlaceOfInterestRequest
        {
            PublicUniqueToken = "CantBeNine"
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(UpdatePlaceOfInterestRequest.EndLocation));
    }
}