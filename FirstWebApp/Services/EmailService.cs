using System.Net;
using System.Net.Mail;

namespace FirstWebApp.Services
{
    public class EmailService: IEmailService
    {

        public EmailService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            
        }

        public void SendEmail()
        {
            // Create the email
            var message = new MailMessage();
            message.From = new MailAddress("sender@example.com");
            message.To.Add("recipient@example.com");
            message.Subject = "Test Email";
            message.Body = "Hello! This is a demo email sent from C#.";
            message.IsBodyHtml = false;

            // Configure SMTP client
            var smtpClient = new SmtpClient("smtp.example.com", 587)
            {
                Credentials = new NetworkCredential("username", "password"),
                EnableSsl = true
            };

            // Send the email
            smtpClient.Send(message);
        }
    }
}
