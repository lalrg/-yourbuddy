﻿namespace YourBuddyPull.Application.Contracts.EmailSender;

public interface IEmailSender
{
    public Task SendMail(string email, string text);
    public Task<bool> SendResetPasswordEmail(string email, string newPassword);
    public Task<bool> SendAccountCreated(string email, string password);
}
