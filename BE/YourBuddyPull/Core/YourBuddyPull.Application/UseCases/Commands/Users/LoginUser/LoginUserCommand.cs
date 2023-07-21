using MediatR;

namespace YourBuddyPull.Application.UseCases.Commands.Users.LoginUser;

public class LoginUserCommand : IRequest<string?>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
