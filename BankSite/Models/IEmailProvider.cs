using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace BankSite.Models
{
    public interface IEmailProvider
    {
        Task SendEmailAsync(string toEmail, string subject, string content, string htmlContent);
    }

    public class EmailProviderSendGrid : IEmailProvider
    {
        private readonly IConfiguration _config;
        public EmailProviderSendGrid(IConfiguration config)
        {
            _config = config;
        }

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
