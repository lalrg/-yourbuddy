using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Users.UpdatePassword;

public class UpdatePasswordCommand: IRequest<bool>
{
    public Guid UserId { get; set; }
    public string NewPassword { get; set; }
    public string OldPassword { get; set; }
}
