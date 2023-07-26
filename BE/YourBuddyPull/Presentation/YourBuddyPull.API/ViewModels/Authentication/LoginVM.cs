using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Authentication;

public class LoginVM
{
    [EmailAddress, Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
