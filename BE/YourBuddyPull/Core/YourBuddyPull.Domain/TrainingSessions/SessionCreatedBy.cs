using YourBuddyPull.Domain.Shared;
using YourBuddyPull.Domain.Shared.Exceptions;

namespace YourBuddyPull.Domain.TrainingSessions;
public sealed class SessionCreatedBy
{
    private SessionCreatedBy(Guid createdById, string createdByName) {
        CreatedById = createdById;
        CreatedByName = createdByName;
    }
    public Guid CreatedById { get; private set; }
    public string CreatedByName { get; private set; }
    public static SessionCreatedBy Instanciate(Guid createdById, string createdByName)
    {
        if (createdById == Guid.Empty)
            throw new DomainValidationException("The createdById of the SessionCreatedBy cannot be empty");
        if (createdByName == string.Empty)
            throw new DomainValidationException("The createdByName of the SessionCreatedBy cannot be empty");

        return new SessionCreatedBy(createdById, createdByName);
    }
}
