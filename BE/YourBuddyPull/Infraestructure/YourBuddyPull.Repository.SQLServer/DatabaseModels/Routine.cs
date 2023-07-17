using System;
using System.Collections.Generic;

namespace YourBuddyPull.Repository.SQLServer.DatabaseModels;

public partial class Routine
{
    public Guid Id { get; set; }

    public Guid? UserAssignedId { get; set; }

    public Guid? CreatedBy { get; set; }

    public bool? IsEnabled { get; set; }

    public string? Name { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<ExerciseRoutine> ExerciseRoutines { get; set; } = new List<ExerciseRoutine>();

    public virtual User? UserAssigned { get; set; }
}
