using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.Contracts.Security;

namespace YourBuddyPull.Application.UseCases.Commands.Users.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponse?>
{
    private readonly IAuthenticationProvider _authProvider;
    private readonly IUserRepository _userRepository;
    public LoginUserHandler(IAuthenticationProvider authProvider, IUserRepository userRepository)
    {
        _authProvider = authProvider;
        _userRepository = userRepository;
    }

    public async Task<LoginUserResponse?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _authProvider.Authenticate(request.Email, request.Password);
        
        if(!result)
            return null;

        var userInfo = await _userRepository.GetUserPropertiesByUsername(request.Email);

        return new LoginUserResponse
        {
            Email = userInfo.Email,
            LastName = userInfo.LastName,
            Name = userInfo.Name,
            Roles = userInfo.Roles
        };
    }
}
