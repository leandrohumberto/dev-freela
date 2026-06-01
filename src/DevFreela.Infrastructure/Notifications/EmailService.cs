using DevFreela.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DevFreela.Infrastructure.Notifications
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridClient _client;
        private readonly string? _fromEmail;
        private readonly string? _fromName;

        public EmailService(ISendGridClient client, IConfiguration configuration)
        {
            _client = client;
            var sendGridSettings = configuration.GetSection("SendGrid");
            _fromEmail = sendGridSettings["FromEmail"];
            _fromName = sendGridSettings["FromName"];
        }

        public async Task SendAsync(string email, string subject, string body, CancellationToken cancellationToken = default)
        {
            var sendGridMessage = new SendGridMessage
            {
                From = new EmailAddress(_fromEmail, _fromName),
                Subject = subject,
            };

            sendGridMessage.AddContent(MimeType.Text, body);
            sendGridMessage.AddTo(new EmailAddress(email));

            //_ = await _client.SendEmailAsync(sendGridMessage, cancellationToken);
        }
    }
}
