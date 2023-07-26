using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.TrainingSession;

public class UpdateTrainingSessionVM
{
    [Required]
    public Guid SessionId { get; set; }
    [Required]
    public DateTime startTime { get; set; }
    public DateTime? endTime { get; set; }
    public List<UpdatedExercise> exercises { get; set; } = new();
}

public class UpdatedExercise
{
    [Required]
    public Guid exerciseId { get; set; }
    [Required]
    public int reps { get; set; }
    [Required]
    public int sets { get; set; }
    [Required]
    public int load { get; set; }
}