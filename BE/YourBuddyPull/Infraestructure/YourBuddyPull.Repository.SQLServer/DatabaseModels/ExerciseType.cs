using System;
using System.Collections.Generic;

namespace YourBuddyPull.Repository.SQLServer.DatabaseModels;

public partial class ExerciseType
{
    public int Id { get; set; }

    public int? MeasurementId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual MeasurementUnit? Measurement { get; set; }
}
