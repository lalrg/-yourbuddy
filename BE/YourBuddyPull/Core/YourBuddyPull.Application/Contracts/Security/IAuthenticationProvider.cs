using System;

namespace YourBuddyPull.Application.Contracts.Security;

public interface IAuthenticationProvider
{
    public string GenerateJWT(
        string email,
        string name,
        Guid id,
        List<string> roles); 
    public (string, string) GenerateNewRandomPasswordAndSalt(); 
}
