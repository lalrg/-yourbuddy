using YourBuddyPull.Domain.Shared;
using YourBuddyPull.Domain.Shared.Exceptions;

namespace YourBuddyPull.Domain.Users;

public sealed class Role: BaseEntity
{
    private Role(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public string Name { get; private set; }

    public static Role Instanciate(Guid id, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainValidationException("The name of the role cannot be null");
        }
        return new(id, name);
    }
}

