using System.ComponentModel.DataAnnotations;

namespace YourBuddyPull.API.ViewModels.User;

public class UpdateUserVM
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
}
