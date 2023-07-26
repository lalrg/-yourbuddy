using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.TrainingSession;

public class CreateTrainingSessionVM
{
    [Required]
    public Guid CreatedBy { get; set; }
    [Required]
    public DateTime startTime { get; set; }
    [Required]
    public Guid RoutineFrom { get; set; }
    public DateTime? endTime { get; set; }
}
