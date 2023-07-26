using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Routine;

public class RemoveExerciseVM
{
    [Required]
    public Guid exerciseId { get; set; }
    [Required]
    public Guid routineId { get; set; }
}
