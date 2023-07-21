using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.Authentication;

public class LoginVM
{
    [EmailAddress, Required]
    public string Email;
    [Required]
    public string Password;
}
