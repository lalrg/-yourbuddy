using YourBuddyPull.Application.Contracts.Security;

namespace YourBuddyPull.Infraestructure.AuthenticationProvider;

public class AuthenticationProvider : IAuthenticationProvider
{
    public Task<string> Authenticate(string email, string password, string name, List<string> roles)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GenerateNewPassword(string username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePassword(string username, string password, string newPasword)
    {
        throw new NotImplementedException();
    }
}
