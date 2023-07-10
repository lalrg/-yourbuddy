namespace YourBuddyPull.Application.DTOs.User;

public struct UserInformationDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsDeleted { get; set; }
    public List<string> Roles { get; set; }
}
