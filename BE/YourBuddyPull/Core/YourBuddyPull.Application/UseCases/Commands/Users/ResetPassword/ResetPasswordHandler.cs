using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.Contracts.Security;

namespace YourBuddyPull.Application.UseCases.Commands.Users.ResetPassword;

public class ResetPasswordHandler: IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly IUserRepository _userRepository;
    public ResetPasswordHandler(IAuthenticationProvider authenticationProvider, IUserRepository userRepository)
    {
        _authenticationProvider = authenticationProvider;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserPropertiesByGuid(request.UserId);
        if (user.Id == Guid.Empty)
            return false;

        return await _authenticationProvider.GenerateNewPassword(user.Email);
    }
}
