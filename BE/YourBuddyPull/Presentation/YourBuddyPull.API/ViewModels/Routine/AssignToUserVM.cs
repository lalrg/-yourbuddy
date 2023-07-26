using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Routine;

public class AssignToUserVM
{
    [Required]
    public Guid userId { get; set; }
    [Required]
    public Guid routineId { get; set; }
}
