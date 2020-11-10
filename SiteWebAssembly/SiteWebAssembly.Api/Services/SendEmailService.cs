using SendGrid;
using SendGrid.Helpers.Mail;
using SiteWebAssembly.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteWebAssembly.Api.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly ISendGridClient _sendGridClient;

        public SendEmailService(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));
        }

        public async Task<bool> SendEmail(Contact contact)
        {
            SendGridMessage msg = new SendGridMessage();
            EmailAddress from = new EmailAddress(contact.Email, contact.Name);
            List<EmailAddress> recipients = new List<EmailAddress> { new EmailAddress("your@email.com", "Your Name") };

            msg.SetSubject("A new user has registered");
            msg.SetFrom(from);
            msg.AddTos(recipients);
            msg.PlainTextContent = contact.Message;

            Response response = await _sendGridClient.SendEmailAsync(msg);
            if (Convert.ToInt32(response.StatusCode) >= 400)
            {
                return false;
            }
            return true;
        }
    }
}
