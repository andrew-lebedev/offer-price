using Microsoft.Extensions.Options;
using OfferPrice.Notifications.Domain.Interfaces;
using OfferPrice.Notifications.Domain.Models;
using OfferPrice.Notifications.Infrastructure.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;

namespace OfferPrice.Notifications.Infrastructure.Services;

public class EmailProviderService : IEmailProviderService
{
    private readonly EmailOptions _emailOptions;
    private readonly ILogger<EmailProviderService> _logger;

    public EmailProviderService(
        IOptions<EmailOptions> options,
        ILogger<EmailProviderService> logger)
    {
        _emailOptions = options.Value;
        _logger = logger;
    }

    public async Task SendEmail(string emailReciever, Notification notification)
    {
        try
        {
            using var smtpClient = new SmtpClient(_emailOptions.ServerDomain, _emailOptions.Port);

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_emailOptions.AppName, _emailOptions.Password);
            smtpClient.EnableSsl = true;

            var message = new MailMessage(_emailOptions.AppName, emailReciever)
            {
                Subject = notification.Subject,
                Body = notification.Body
            };

            await smtpClient.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception while sending email: {ex.Message}");
        }

    }
}
