using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.Contracts.Security;

namespace YourBuddyPull.Application.UseCases.Commands.Users.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, string?>
{
    private readonly IAuthenticationProvider _authProvider;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public LoginUserHandler(IAuthenticationProvider authProvider, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _authProvider = authProvider;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;

    }

    public async Task<string?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var userInfo = await _userRepository.GetUserPropertiesByUsername(request.Email);

        var name = $"{userInfo.Name} {userInfo.LastName}";

        var result = await _authProvider.Authenticate(request.Email, request.Password, name, userInfo.Roles);   
        
        if (string.IsNullOrEmpty(result))
            return null;


        return result;
    }
}
