using System.Net;
using System.Net.Mail;

namespace SignalR.Domain.Helper
{
    public class MailHelper
    {
        public static async Task Send(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            var fromEmail = "zomra.info.2024@gmail.com";
            var fromPassword = "gtwghidjwjehbetl";

            var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
            };

            smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(message);
        }
    }
}
