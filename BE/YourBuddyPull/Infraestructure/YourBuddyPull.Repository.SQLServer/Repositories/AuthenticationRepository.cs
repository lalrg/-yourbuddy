using Microsoft.EntityFrameworkCore;
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

        var stringSalt = user.PasswordSalt;
        var stringPasswordHash = user.PasswordHash;

        return (stringPasswordHash, stringSalt);
    }

    public async Task<(string, string)> GetHashAndSaltByEmail(string email)
    {
        var user = await _context.Users.SingleAsync(user => user.Email == email);

        var stringSalt = user.PasswordSalt;
        var stringPasswordHash = user.PasswordHash;

        return (stringPasswordHash, stringSalt);
    }

    public async Task<bool> UpdatePassword(Guid userId, string password, string salt)
    {
        var user = await _context.Users.SingleAsync(user => user.Id == userId);
        user.PasswordSalt = salt;
        user.PasswordHash = password;

        _context.Entry(user).State = EntityState.Modified;

        return true;
    }
}
