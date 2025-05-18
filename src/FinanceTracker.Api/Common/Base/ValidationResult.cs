namespace FinanceTracker.Api.Common.Base;

public class ValidationResult(IEnumerable<Error> errors)
{
    public bool IsSuccess { get; set; } = false;
    public IEnumerable<Error> ValidationErrors { get; set; } = errors;

    public static ValidationResult Failure(IEnumerable<Error> errors) =>
        new(errors);
}
