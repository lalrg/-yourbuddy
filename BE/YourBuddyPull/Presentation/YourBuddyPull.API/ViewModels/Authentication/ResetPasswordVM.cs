using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Authentication;

public class ResetPasswordVM
{
    [Required]
    public Guid UserId { get; set; }
}
