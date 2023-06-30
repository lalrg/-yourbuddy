using YourBuddyPull.Domain.Shared.BaseClasses;
using YourBuddyPull.Domain.Shared.Exceptions;

namespace YourBuddyPull.Domain.Shared.ValueObjects;
public sealed class CreatedBy : ValueObject
{
    private CreatedBy(Guid createdById, string createdByName)
    {
        CreatedById = createdById;
        CreatedByName = createdByName;
    }
    public Guid CreatedById { get; private set; }
    public string CreatedByName { get; private set; }
    public static CreatedBy Instanciate(Guid createdById, string createdByName)
    {
        if (createdById == Guid.Empty)
            throw new DomainValidationException("The createdById of the SessionCreatedBy cannot be empty");
        if (createdByName == string.Empty)
            throw new DomainValidationException("The createdByName of the SessionCreatedBy cannot be empty");

        return new CreatedBy(createdById, createdByName);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CreatedById;
        yield return CreatedByName;
    }
}
