namespace YourBuddyPull.Application.Contracts.EmailSender;

public interface IEmailSender
{
    public Task<bool> SendResetPasswordEmail(string newPassword);
    public Task<bool> SendAccountCreated(string user, string password);
}
