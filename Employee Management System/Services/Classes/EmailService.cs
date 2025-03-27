using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Employee_Management_System.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string token)
        {
            // Fetch email configuration from appsettings.json
            var smtpServer = _config["EmailSettings:SMTPServer"];
            var smtpPort = int.Parse(_config["EmailSettings:Port"]);
            var senderEmail = _config["EmailSettings:SenderEmail"];
            var senderPassword = _config["EmailSettings:SenderPassword"];

            string resetLink = $"https://your-app.com/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}";

            string body = $@"
          <p>Here are your login credentials:</p>
          <p><b>Email:</b> {email}</p>
          <p><b>Token:</b> {token}</p>
          <p><b>Token Validity:</b> 15 Minutes</p>
          <p>Please keep these credentials safe and do not share them with anyone.</p>
         <p>You can reset your password using the following link:</p>
         <a href='{resetLink}'>Reset Password</a>
        ";

            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, "Support Team"),
                    Subject = "Password Reset Request",
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);



            }
        }
    }

}
