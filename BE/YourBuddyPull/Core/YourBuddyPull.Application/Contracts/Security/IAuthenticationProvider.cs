using System;

namespace YourBuddyPull.Application.Contracts.Security;

public interface IAuthenticationProvider
{
    public string GenerateJWT(
        string email,
        string name,
        List<string> roles); 
    public (string, string) GenerateNewRandomPasswordAndSalt(); 
}
