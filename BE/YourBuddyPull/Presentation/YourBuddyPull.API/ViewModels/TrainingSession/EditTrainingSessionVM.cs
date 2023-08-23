using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.TrainingSession;

public class UpdateTrainingSessionVM
{
    [Required]
    public Guid SessionId { get; set; }
    [Required]
    public DateTime startTime { get; set; }
    public DateTime? endTime { get; set; }
}