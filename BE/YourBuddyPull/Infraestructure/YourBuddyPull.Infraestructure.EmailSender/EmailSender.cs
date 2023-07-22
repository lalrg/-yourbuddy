using System.Net.Mail;
using YourBuddyPull.Application.Contracts.EmailSender;

namespace YourBuddyPull.Infraestructure.EmailSender;

public class EmailSender : IEmailSender
{
    public Task<bool> SendAccountCreated(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SendResetPasswordEmail(string email, string newPassword)
    {
        throw new NotImplementedException();
    }

    private Task<bool> SendMail(string email, string message)
    {
        string addressFrom = "";
        string passwordFrom = "";

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