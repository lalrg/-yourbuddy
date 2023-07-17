using System;
using System.Collections.Generic;

namespace YourBuddyPull.Repository.SQLServer.DatabaseModels;

public partial class ExerciseRoutine
{
    public Guid RoutineId { get; set; }

    public Guid ExerciseId { get; set; }

    public int? Load { get; set; }

    public int? Sets { get; set; }

    public int? Reps { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual Routine Routine { get; set; } = null!;
}
