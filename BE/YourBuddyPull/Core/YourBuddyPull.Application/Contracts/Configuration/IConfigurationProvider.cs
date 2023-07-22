namespace YourBuddyPull.Application.Contracts.Configuration;

public interface IConfigurationProvider
{
    string MailSenderPassword();
    string MailSenderUsername();
}
