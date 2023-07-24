using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourBuddyPull.Application.Contracts.Security;

public interface IAuthenticationRepository
{
    Task<(string, string)> GetHashAndSalt(Guid userId);
    Task<(string, string)> GetHashAndSaltByEmail(string email);
    Task<bool> UpdatePassword(Guid userId, string password, string salt);
}
