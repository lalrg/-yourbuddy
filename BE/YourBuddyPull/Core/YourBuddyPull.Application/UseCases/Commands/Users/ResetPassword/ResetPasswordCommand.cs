using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Users.ResetPassword;

public class ResetPasswordCommand: IRequest<bool>
{
    public Guid UserId { get; set; }
}
