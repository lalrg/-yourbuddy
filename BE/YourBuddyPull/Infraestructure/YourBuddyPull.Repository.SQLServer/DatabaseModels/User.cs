using System;
using System.Collections.Generic;

namespace YourBuddyPull.Repository.SQLServer.DatabaseModels;

public partial class User
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public string? Email { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Routine> RoutineCreatedByNavigations { get; set; } = new List<Routine>();

    public virtual ICollection<Routine> RoutineUserAssigneds { get; set; } = new List<Routine>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
