namespace TurboKart.Domain.Exceptions;

public class NotEnoughSpaceException : ArgumentException
{
    public NotEnoughSpaceException()
    {
    }

    public NotEnoughSpaceException(string? message) : base(message)
    {
    }

    public NotEnoughSpaceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}