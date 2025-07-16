using FluentValidation.Results;
using PlaceOfInterest.Application.Extensions;

namespace PlaceOfInterest.Application.Tests.Extensions;

public class ValidationResultExtensionsTests
{
    [Fact]
    public void GetValidationFailures_GetErrorList_ReturnString()
    {
        var validationFailures = new List<ValidationFailure>
        {
            new("Property1", "Error message 1"),
            new("Property2", "Error message 2")
        };

        var validationResult = new ValidationResult(validationFailures);
        var result = validationResult.GetValidationFailures();

        Assert.Multiple(() =>
        {
            Assert.True(result.IndexOf("Error message 1") >= 0);
            Assert.True(result.IndexOf("Error message 2") >= 0);
        });
    }
}