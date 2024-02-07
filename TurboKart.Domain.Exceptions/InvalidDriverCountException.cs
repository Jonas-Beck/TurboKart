namespace TurboKart.Domain.Exceptions;

public class InvalidDriverCountException : ArgumentOutOfRangeException
{
    public InvalidDriverCountException()
    {
    }

    public InvalidDriverCountException(string? message) : base(message)
    {
    }

    public InvalidDriverCountException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}