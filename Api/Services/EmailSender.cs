using Api.Shared;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Api.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfigurationHelper _configuration;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfigurationHelper configuration, ILogger<EmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration.Smtp.FromSubject, _configuration.Smtp.FromEmail));
            message.To.Add(new MailboxAddress(subject, email));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlMessage };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(
                    _configuration.Smtp.Host,
                    _configuration.Smtp.Port,
                    false);

                await client.AuthenticateAsync(
                    _configuration.Smtp.UserName, 
                    _configuration.Smtp.Password);

                _logger.LogInformation("Sending email to {Email} with subject {Subject}", email, subject);

                await client.SendAsync(message);
            }
        }
    }
}
