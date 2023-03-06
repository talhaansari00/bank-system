using System;
namespace BankSystem.Services;

public interface IEmailSender
{
    Task SendAsync(string to, string subject, string htmlBody);
}
