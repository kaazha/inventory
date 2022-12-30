using System.Net.Mail;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.SharedKernel;

namespace Aine.Inventory.Infrastructure;

[Inject(Environment = "Production")]
public class SmtpEmailSender : IEmailSender
{
  private readonly ILogger _logger;

  public SmtpEmailSender(ILogger logger)
  {
    _logger = logger;
  }

  public async Task SendEmailAsync(string to, string from, string subject, string body)
  {
    var emailClient = new SmtpClient("localhost");
    var message = new MailMessage
    {
      From = new MailAddress(from),
      Subject = subject,
      Body = body
    };
    message.To.Add(new MailAddress(to));
    await emailClient.SendMailAsync(message);
    _logger.Warn("Sending email to {to} from {from} with subject {subject}.", to, from, subject);
  }
}

