using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.TrainingSession;

public class UpdateTrainingSessionVM
{
    [Required]
    public Guid SessionId;
    [Required]
    public DateTime startTime;
    public DateTime? endTime;
    public List<UpdatedExercise> exercises = new();
}

public class UpdatedExercise
{
    [Required]
    public Guid exerciseId;
    [Required]
    public int reps;
    [Required]
    public int sets;
    [Required]
    public int load;
}