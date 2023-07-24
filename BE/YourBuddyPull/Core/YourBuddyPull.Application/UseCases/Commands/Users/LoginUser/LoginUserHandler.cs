using MediatR;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.Contracts.Security;
using static System.Net.Mime.MediaTypeNames;

namespace YourBuddyPull.Application.UseCases.Commands.Users.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, string?>
{
    private readonly IAuthenticationProvider _authProvider;
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly IUserRepository _userRepository;
    public LoginUserHandler(
        IAuthenticationProvider authProvider, 
        IUserRepository userRepository, 
        IAuthenticationRepository authenticationRepository)
    {
        _authProvider = authProvider;
        _userRepository = userRepository;
        _authenticationRepository = authenticationRepository;   

    }

    public async Task<string?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var userInfo = await _userRepository.GetUserPropertiesByUsername(request.Email);
        var name = $"{userInfo.Name} {userInfo.LastName}";
        var (hash, salt) = await _authenticationRepository.GetHashAndSalt(userInfo.Id);

        var sha = new System.Security.Cryptography.SHA256Managed();
        byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(request.Password + salt);
        byte[] hashBytes = sha.ComputeHash(textBytes);

        var newHash = BitConverter
            .ToString(hashBytes)
            .Replace("-", String.Empty);

        var result = newHash == hash;

        if (!result)
            return null;

        var token = _authProvider.GenerateJWT(request.Email, name, userInfo.Roles);

        return token;
    }
}
