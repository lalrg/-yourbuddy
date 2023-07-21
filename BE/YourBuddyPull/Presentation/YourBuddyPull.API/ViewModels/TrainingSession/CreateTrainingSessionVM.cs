using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.TrainingSession;

public class CreateTrainingSessionVM
{
    [Required]
    public Guid CreatedBy;
    [Required]
    public DateTime startTime;
    [Required]
    public Guid RoutineFrom;
    public DateTime? endTime;
}
