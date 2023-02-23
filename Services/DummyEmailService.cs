using System;

namespace BankSystem.Services
{
	public class DummyEmailSender : IEmailSender
	{
        public Task SendAsync(string to, string subject, string htmlBody)
        {
            throw new NotImplementedException();
        }
    }
}

