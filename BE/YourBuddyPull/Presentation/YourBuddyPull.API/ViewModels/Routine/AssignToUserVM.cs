using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Routine;

public class AssignToUserVM
{
    [Required]
    public Guid userId;
    [Required]
    public Guid routineId;
}
