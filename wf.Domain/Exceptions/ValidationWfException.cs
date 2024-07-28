namespace wf.Domain.Exceptions;

public sealed class ValidationWfException : Exception
{
    public string? ErrorCode { get; init; }

    public ValidationWfException()
    {
    }

    public ValidationWfException(string message) : base(message)
    {
    }

    public ValidationWfException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public ValidationWfException(string message, Exception inner) : base(message, inner)
    {
    }
}
