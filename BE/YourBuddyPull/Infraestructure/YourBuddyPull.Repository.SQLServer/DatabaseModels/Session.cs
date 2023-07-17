using System;
using System.Collections.Generic;

namespace YourBuddyPull.Repository.SQLServer.DatabaseModels;

public partial class Session
{
    public Guid Id { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<SessionExercise> SessionExercises { get; set; } = new List<SessionExercise>();
}
