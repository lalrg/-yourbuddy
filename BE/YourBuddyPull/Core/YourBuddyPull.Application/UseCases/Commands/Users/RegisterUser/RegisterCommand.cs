using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Users.RegisterUser;

public class RegisterCommand: IRequest<bool>
{
    public string Name;
    public string LastName;
    public string Email;
    public List<string> Roles;
}
