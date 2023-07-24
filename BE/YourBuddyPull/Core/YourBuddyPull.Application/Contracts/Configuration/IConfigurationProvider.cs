namespace YourBuddyPull.Application.Contracts.Configuration;

public interface IConfigurationProvider
{
    string MailSenderPassword();
    string MailSenderUsername();
    public string JwtIssuer();
    public string JwtAudience();
    public string JwtSecurityKey();
    public string JwtLifespanInMinutes();
}
