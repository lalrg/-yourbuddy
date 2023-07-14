using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.Contracts.Security;

namespace YourBuddyPull.Application.UseCases.Commands.Users.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponse?>
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

    public async Task<LoginUserResponse?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _authProvider.Authenticate(request.Email, request.Password);
        
        if(!result)
            return null;

        _unitOfWork.OpenTransaction();
        var userInfo = await _userRepository.GetUserPropertiesByUsername(request.Email);
        _unitOfWork.CommitTransaction();

        return new LoginUserResponse
        {
            Email = userInfo.Email,
            LastName = userInfo.LastName,
            Name = userInfo.Name,
            Roles = userInfo.Roles
        };
    }
}
