namespace YourBuddyPull.Domain.Shared.Exceptions;

public sealed class DomainValidationException: Exception
{
    public DomainValidationException(string message)
        : base(message)
    {

    }
}
