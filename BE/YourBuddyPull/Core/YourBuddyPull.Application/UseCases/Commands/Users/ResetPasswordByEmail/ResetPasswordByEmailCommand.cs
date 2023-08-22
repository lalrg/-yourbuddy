using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Users.ResetPasswordByEmail;

public class ResetPasswordByEmailCommand: IRequest<bool>
{
    public string UserEmail { get; set; }
}
