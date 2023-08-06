using MediatR;
namespace YourBuddyPull.Application.UseCases.Commands.Users.UpdateProperties;

public class UpdatePropertiesCommand: IRequest<bool>
{
    public Guid Id;
    public string Name;
    public string LastName;
    public string Email;
    public string Role;
}
