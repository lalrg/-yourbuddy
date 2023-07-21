using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Routine;

public class RemoveExerciseVM
{
    [Required]
    public Guid exerciseId;
    [Required]
    public Guid routineId;
}
