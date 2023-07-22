using Microsoft.Extensions.Configuration;
using YourBuddyPull.Application.Contracts.Configuration;

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
}
