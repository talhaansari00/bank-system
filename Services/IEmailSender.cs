using System;
namespace BankSystem.Services;

interface IEmailSender
{
    Task SendAsync(string to, string subject, string htmlBody);
}
