using FuerzaG.Domain.Ports;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace FuerzaG.Infrastructure;

public class MailSettings
{
    public string SmtpHost { get; set; } = "";
    public int SmtpPort { get; set; }
    public string SmtpUser { get; set; } = "";
    public string SmtpPass { get; set; } = "";
    public string FromName { get; set; } = "";
    public string FromEmail { get; set; } = "";
}

public class SmtpEmailSender : IMailSender
{
    public void SendEmail(string email, string subject, string htmlMessage)
    {
        private readonly MailSettings _settings;

        public SmtpEmailSender(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }
        
    }
}