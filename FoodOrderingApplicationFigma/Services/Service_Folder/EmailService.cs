using FoodOrderingApplicationFigma.Models;
using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FoodOrderingApplicationFigma.Services.Service_Folder
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = body };

            using var smtp = new SmtpClient();
            
            // Set timeout for cloud deployment
            smtp.Timeout = 30000; // 30 seconds
            
            try
            {
                // Use SSL for port 465, StartTls for port 587
                var secureOptions = _emailSettings.SmtpPort == 465 
                    ? MailKit.Security.SecureSocketOptions.SslOnConnect 
                    : MailKit.Security.SecureSocketOptions.StartTls;
                    
                await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, secureOptions);
                await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Email sending failed: {ex.Message}");
            }
        }
    }
}
