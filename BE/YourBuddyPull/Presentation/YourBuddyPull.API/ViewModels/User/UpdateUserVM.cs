using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.User;

public class UpdateUserVM
{
    [Required]
    public string Name;
    [Required]
    public string LastName;
    [Required, EmailAddress]
    public string Email;
}
