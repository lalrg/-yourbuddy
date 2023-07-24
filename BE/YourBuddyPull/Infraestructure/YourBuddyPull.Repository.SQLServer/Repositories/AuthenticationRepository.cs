using Microsoft.EntityFrameworkCore;
using System.Text;
using YourBuddyPull.Application.Contracts.Security;

namespace YourBuddyPull.Repository.SQLServer.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly ProyectoLuisRContext _context;
    public AuthenticationRepository(ProyectoLuisRContext context)
    {
        _context = context;
    }

    public async Task<(string, string)> GetHashAndSalt(Guid userId)
    {
        var user = await _context.Users.SingleAsync(user => user.Id == userId);

        return (user.PasswordHash.ToString(), user.PasswordSalt.ToString());
    }

    public async Task<(string, string)> GetHashAndSaltByEmail(string email)
    {
        var user = await _context.Users.SingleAsync(user => user.Email == email);

        return (user.PasswordHash.ToString(), user.PasswordSalt.ToString());
    }

    public async Task<bool> UpdatePassword(Guid userId, string password, string salt)
    {
        var user = await _context.Users.SingleAsync(user => user.Id == userId);
        user.PasswordSalt = Encoding.ASCII.GetBytes(salt);
        user.PasswordHash = Encoding.ASCII.GetBytes(password);

        _context.Entry(user).State = EntityState.Modified;

        return true;
    }
}
