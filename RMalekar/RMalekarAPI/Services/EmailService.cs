using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace RMalekarAPI.Services
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpAppPassword;
        private readonly string _smtpDomain;
        private readonly string _fromEmail;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _smtpServer = configuration["SMTP:Server"]!;
            _smtpPort = int.Parse(configuration["SMTP:Port"]!);
            _smtpUsername = configuration["SMTP:Username"]!;
            _smtpAppPassword = configuration["SMTP:AppPassword"]!;
            _smtpDomain = configuration["SMTP:Domain"]!;
            _fromEmail = configuration["SMTP:FromEmail"]!;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_smtpServer))
            {
                try
                {
                    client.Port = _smtpPort;
                    client.Credentials = new NetworkCredential(_smtpUsername, _smtpAppPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage(_fromEmail, toEmail, subject, body);

                    mailMessage.IsBodyHtml = true;

                    await client.SendMailAsync(mailMessage);
                } catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex.Message);
                }
            }
        }
    }
}
