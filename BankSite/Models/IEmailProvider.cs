using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace BankSite.Models
{
    /// <summary>
    /// An interface to manage sending emails.
    /// </summary>
    public interface IEmailProvider
    {
        Task SendEmailAsync(string toEmail, string subject, string content, string htmlContent);
    }


    /// <summary>
    /// An implementation of <see cref="IEmailProvider"/> using SendGrid.
    /// </summary>
    public class EmailProviderSendGrid : IEmailProvider
    {
        private readonly IConfiguration _config;
        public EmailProviderSendGrid(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Sends an email using SendGrid.
        /// </summary>
        /// <param name="toEmail">The email address of the recipient</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="content">The plain text content of the email</param>
        /// <param name="htmlContent">The html content of the email</param>
        /// <returns></returns>
        public async Task SendEmailAsync(string toEmail, string subject, string content, string htmlContent)
        {
            var apiKey = _config.GetSection("SendGridKey").Value;
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_config.GetSection("FromEmail").Value, "MitchellCodes Team"),
                Subject = subject,
                PlainTextContent = content,
                HtmlContent = htmlContent
            };
            msg.AddTo(new EmailAddress(toEmail));
            await client.SendEmailAsync(msg);
            //var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
