using Microsoft.Extensions.Configuration;

namespace YourBuddyPull.Infraestructure.ConfigurationProvider;

public class ConfigurationProvider : Application.Contracts.Configuration.IConfigurationProvider
{
    private readonly IConfiguration  _configuration;
    public ConfigurationProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string MailSenderPassword()
    {
        return _configuration.GetSection("smtpPassword").Value;
    }

    public string MailSenderUsername()
    {
        return _configuration.GetSection("smtpUser").Value;
    }

    public string JwtIssuer()
    {
        return _configuration.GetSection("JWT:issuer").Value;
    }

    public string JwtAudience()
    {
        return _configuration.GetSection("JWT:audience").Value;
    }

    public string JwtSecurityKey()
    {
        return _configuration.GetSection("JWT:key").Value;
    }

    public string JwtLifespanInMinutes()
    {
        return _configuration.GetSection("JWT:lifespanInMinutes").Value;
    }
}
