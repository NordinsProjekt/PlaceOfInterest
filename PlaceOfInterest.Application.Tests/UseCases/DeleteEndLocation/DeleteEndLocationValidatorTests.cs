using PlaceOfInterest.Application.UseCases.DeleteEndLocation;

namespace PlaceOfInterest.Application.Tests.UseCases.DeleteEndLocation;

public class DeleteEndLocationValidatorTests
{
    private DeleteEndLocationRequest CreateRequest()
    {
        return new DeleteEndLocationRequest
        {
            Id = Guid.NewGuid(),
            Count = 0
        };
    }

    [Fact]
    public void Validator_AcceptedRequest_ShouldValidate()
    {
        var validator = new DeleteEndLocationValidator();
        var request = CreateRequest();

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validator_IdGuidIsEmpty_ShouldNotValidate()
    {
        var validator = new DeleteEndLocationValidator();
        var request = CreateRequest();
        request.Id = Guid.Empty;

        var result = validator.Validate(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(DeleteEndLocationRequest.Id));
        });
    }

    [Fact]
    public void Validator_CountIsHigherThanZero_ShouldNotValidate()
    {
        var validator = new DeleteEndLocationValidator();
        var request = CreateRequest();
        request.Count = 1;

        var result = validator.Validate(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(DeleteEndLocationRequest.Count));
        });
    }
}