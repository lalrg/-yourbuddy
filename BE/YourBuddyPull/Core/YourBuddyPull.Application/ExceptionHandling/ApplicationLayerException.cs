namespace YourBuddyPull.Application.ExceptionHandling;

public sealed class ApplicationException : Exception
{
    public ApplicationException(string message)
        : base(message)
    {

    }
}

