namespace HomeInventory.Application.Common.Exceptions;

public class ValidationException : AppException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("Validation failed", "VALIDATION_ERROR", 400)
    {
        Errors = errors;
    }
}