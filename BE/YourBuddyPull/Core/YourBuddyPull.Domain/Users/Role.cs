﻿using YourBuddyPull.Domain.Shared.BaseClasses;
using YourBuddyPull.Domain.Shared.Exceptions;

namespace YourBuddyPull.Domain.Users;

public sealed class Role : ValueObject
{
    private Role(string name)
    {
        Name = name;
    }
    public string Name { get; private set; }

    public static Role Instanciate(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainValidationException("The name of the role cannot be null");
        }
        return new(name);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}

