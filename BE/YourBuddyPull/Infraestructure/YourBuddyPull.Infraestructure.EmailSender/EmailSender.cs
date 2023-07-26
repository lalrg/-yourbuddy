using System.Net.Mail;
using YourBuddyPull.Application.Contracts.Configuration;
using YourBuddyPull.Application.Contracts.EmailSender;

namespace YourBuddyPull.Infraestructure.EmailSender;

public class EmailSender : IEmailSender
{
    private readonly IConfigurationProvider _configurationProvider;
    public EmailSender(IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
    }
    public async Task<bool> SendAccountCreated(string email, string password)
    {
        return await SendMail(email, MessageBuilder.BuildAccountCreatedMessage(email, password));
    }

    public async Task<bool> SendResetPasswordEmail(string email, string newPassword)
    {
        return await SendMail(email, MessageBuilder.BuildResetPasswordEmail(email, newPassword));
    }

    private Task<bool> SendMail(string email, string message)
    {
        string addressFrom = _configurationProvider.MailSenderUsername();
        string passwordFrom = _configurationProvider.MailSenderPassword();

        MailMessage msg = new MailMessage();
        msg.To.Add(new MailAddress(email));
        msg.From = new MailAddress(addressFrom);
        msg.Subject = "YourBuddy Notifications";
        msg.Body = message;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(addressFrom, passwordFrom);
        client.Port = 587;
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;
        client.Send(msg);

        return Task.FromResult(true);
    }
}

internal static class MessageBuilder
{
    public static string BuildAccountCreatedMessage(string user, string password)
    {
        return $@"Se ha creado una cuenta para YourBuddy. Usuario: {user}  Contraseña: {password}";
    }
    public static string BuildResetPasswordEmail(string email, string newPassword)
    {
        return $@"Se ha actualizado su Email para YourBuddy. Contraseña: {newPassword}";
    }
}