using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Routine;

public class AddExerciseVM
{
    [Required]
    public Guid ExerciseId { get; set; }
    [Required]
    public Guid RoutineId { get; set; }
    [Required]
    public int reps { get; set; }
    [Required]
    public int load { get; set; }
    [Required]
    public int sets { get; set; }
}
