using MediatR;
using System.Text;
using XSystem.Security.Cryptography;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.Contracts.Security;
using YourBuddyPull.Application.DTOs.User;

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
        var user = await _userRepository.TryGetUserPropertiesByUsername(request.Email);
        if(user == null)
        {
            return null;
        }

        var userInfo = (UserInformationDTO)user;
        var name = $"{userInfo.Name} {userInfo.LastName}";
        var (hash, salt) = await _authenticationRepository.GetHashAndSalt(userInfo.Id);

        var newHash = GenerateHash(request.Password, salt);

        var result = newHash == hash;

        if (!result)
            return null;

        var token = _authProvider.GenerateJWT(request.Email, name, userInfo.Id, userInfo.Roles);

        return token;
    }

    private string GenerateHash(string input, string salt)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
        SHA256Managed sHA256ManagedString = new SHA256Managed();
        byte[] hash = sHA256ManagedString.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
