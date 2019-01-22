using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace eCommerceSite
{
    public class EmailSender : IEmailSender
    {
        private readonly ISendGridClient _sendGridClient;

        public EmailSender(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("admin@jacesplace.com", "Card Admins"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(email));

            msg.SetClickTracking(false, false);

            return _sendGridClient.SendEmailAsync(msg);
        }

    }
}
