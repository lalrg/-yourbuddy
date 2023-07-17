using System;
using System.Collections.Generic;

namespace YourBuddyPull.Repository.SQLServer.DatabaseModels;

public partial class MeasurementUnit
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ExerciseType> ExerciseTypes { get; set; } = new List<ExerciseType>();
}
