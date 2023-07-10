using System;

namespace YourBuddyPull.Application.Contracts.Security;

public interface IAuthenticationProvider
{
    // we should handle the db call inside this method.
    public Task<bool> Authenticate(string email, string password); 

    // This should also deal with database update
    // after generating a password we should raise an event to send an email.
    public Task<bool> GenerateNewPassword(string username);

    // after generating a password we should raise an event to send a confirmation email.
    // this should also deal with database update.
    public Task<bool> UpdatePassword(string username, string password, string newPasword); 
}
