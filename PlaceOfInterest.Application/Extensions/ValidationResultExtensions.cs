using System.Text;
using FluentValidation.Results;

namespace PlaceOfInterest.Application.Extensions;

public static class ValidationResultExtensions
{
    public static string GetValidationFailures(this ValidationResult result)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Encountered {result.Errors.Count} errors.");
        result.Errors.ForEach(x => sb.AppendLine(x.ErrorMessage));

        return sb.ToString();
    }
}
