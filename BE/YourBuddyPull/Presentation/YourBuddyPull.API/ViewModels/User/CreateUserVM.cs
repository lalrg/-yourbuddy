using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.User;

public class CreateUserVM
{
    [Required]
    public string Name;
    [Required]
    public string LastName;
    [Required, EmailAddress]
    public string Email;
    [Required]
    public List<string> Roles;
}
