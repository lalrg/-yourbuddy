using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Users.DisableUser;

public class DisableUserCommand: IRequest<bool>
{
    public Guid userId { get; set; }
}
