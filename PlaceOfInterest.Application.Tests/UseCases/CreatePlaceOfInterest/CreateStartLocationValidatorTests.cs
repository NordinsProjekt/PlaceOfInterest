using PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;

namespace PlaceOfInterest.Application.Tests.UseCases.CreatePlaceOfInterest;

public class CreatePlaceOfInterestValidatorTests
{
    [Fact]
    public void Validator_AcceptedRequest_ShouldValidate()
    {
        var validator = new CreatePlaceOfInterestValidator();
        var request = new CreatePlaceOfInterestRequest
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
        var validator = new CreatePlaceOfInterestValidator();
        var request = new CreatePlaceOfInterestRequest
        {
            Name = ""
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreatePlaceOfInterestRequest.Name));
    }

    [Fact]
    public void Validator_NameStringIsToLong_ShouldNotValidate()
    {
        var validator = new CreatePlaceOfInterestValidator();
        var request = new CreatePlaceOfInterestRequest
        {
            Name = new string('a', 101)
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreatePlaceOfInterestRequest.Name));
    }

    [Fact]
    public void Validator_StartLocationIsEmpty_ShouldNotValidate()
    {
        var validator = new CreatePlaceOfInterestValidator();
        var request = new CreatePlaceOfInterestRequest
        {
            StartLocation = ""
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors,
            error => error.PropertyName == nameof(CreatePlaceOfInterestRequest.StartLocation));
    }

    [Fact]
    public void Validator_EndLocationIsEmpty_ShouldNotValidate()
    {
        var validator = new CreatePlaceOfInterestValidator();
        var request = new CreatePlaceOfInterestRequest
        {
            EndLocation = ""
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreatePlaceOfInterestRequest.EndLocation));
    }

    [Fact]
    public void Validator_PublicUniqueTokenIsEmpty_ShouldNotValidate()
    {
        var validator = new CreatePlaceOfInterestValidator();
        var request = new CreatePlaceOfInterestRequest
        {
            PublicUniqueToken = ""
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreatePlaceOfInterestRequest.EndLocation));
    }

    [Fact]
    public void Validator_PublicUniqueTokenIsToShort_ShouldNotValidate()
    {
        var validator = new CreatePlaceOfInterestValidator();
        var request = new CreatePlaceOfInterestRequest
        {
            PublicUniqueToken = "Short"
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreatePlaceOfInterestRequest.EndLocation));
    }

    [Fact]
    public void Validator_PublicUniqueTokenIsToLong_ShouldNotValidate()
    {
        var validator = new CreatePlaceOfInterestValidator();
        var request = new CreatePlaceOfInterestRequest
        {
            PublicUniqueToken = "CantBeNine"
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreatePlaceOfInterestRequest.EndLocation));
    }

    [Fact]
    public void Validator_PublicUniqueTokenIs36Chars_ShouldValidate()
    {
        var validator = new CreatePlaceOfInterestValidator();
        var request = new CreatePlaceOfInterestRequest
        {
            PublicUniqueToken = Guid.NewGuid().ToString()
        };

        var result = validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreatePlaceOfInterestRequest.EndLocation));
    }
}