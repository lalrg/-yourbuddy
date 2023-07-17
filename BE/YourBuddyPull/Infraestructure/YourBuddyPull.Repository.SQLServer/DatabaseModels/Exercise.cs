using System;
using System.Collections.Generic;

namespace YourBuddyPull.Repository.SQLServer.DatabaseModels;

public partial class Exercise
{
    public Guid Id { get; set; }

    public int? TypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string? VideoUrl { get; set; }

    public virtual ICollection<ExerciseRoutine> ExerciseRoutines { get; set; } = new List<ExerciseRoutine>();

    public virtual ICollection<SessionExercise> SessionExercises { get; set; } = new List<SessionExercise>();

    public virtual ExerciseType? Type { get; set; }
}
