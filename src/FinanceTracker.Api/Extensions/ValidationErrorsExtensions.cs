using FinanceTracker.Api.Common.Base;

namespace FinanceTracker.Api.Extensions;
public static class ValidationErrorsExtensions
{
    public static IResult ToResult(this IEnumerable<Error> validationErrors)
    {
        var validationResult = ValidationResult.Failure(validationErrors);
        return Results.UnprocessableEntity(validationResult);
    }
}