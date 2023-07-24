using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using YourBuddyPull.Application.Contracts.Security;

namespace YourBuddyPull.Infraestructure.AuthenticationProvider;

public class AuthenticationProvider : IAuthenticationProvider
{
    private readonly Application.Contracts.Configuration.IConfigurationProvider _configurationProvider;
    public AuthenticationProvider(Application.Contracts.Configuration.IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
    }
    public string GenerateJWT(string email, string name, List<string> roles)
    {
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Email, email),
            new (JwtRegisteredClaimNames.Name, name),
            new("roles", string.Join(',',roles))
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configurationProvider.JwtSecurityKey())),
                SecurityAlgorithms.HmacSha256
            );

        var token = new JwtSecurityToken(
            _configurationProvider.JwtIssuer(),
            _configurationProvider.JwtAudience(),
            claims,
            null,
            DateTime.Now.AddMinutes(int.Parse(_configurationProvider.JwtLifespanInMinutes())),
            signingCredentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public (string, string) GenerateNewRandomPasswordAndSalt()
    {
        var password = CreatePassword(10);
        var salt = CreateSalt(50);

        return (password, salt.ToString());
    }

    private string CreatePassword(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }

    private static byte[] CreateSalt(int maximumSaltLength)
    {
        var salt = new byte[maximumSaltLength];
        using (var random = new RNGCryptoServiceProvider())
        {
            random.GetNonZeroBytes(salt);
        }

        return salt;
    }
}
