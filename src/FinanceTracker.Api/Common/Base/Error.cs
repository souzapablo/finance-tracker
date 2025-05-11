namespace FinanceTracker.Api.Common.Base;

public record Error(string Code, string Message)
{
    public static Error ExternalError(string message) => new("EXTERNAL", message);
};