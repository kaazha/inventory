using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.SharedKernel;

namespace Aine.Inventory.Infrastructure;

[Inject(Environment = "Development")]
public class FakeEmailSender : IEmailSender
{
  public Task SendEmailAsync(string to, string from, string subject, string body)
  {
    return Task.CompletedTask;
  }
}

