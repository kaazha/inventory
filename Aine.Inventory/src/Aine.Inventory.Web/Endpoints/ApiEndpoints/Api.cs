using System.Text;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Aine.Inventory.Web.Endpoints.ApiEndpoints;

public class Api : EndpointWithoutRequest<ContentResult>
{
  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/");
    AllowAnonymous();
  }

  public override Task HandleAsync(CancellationToken ct)
  {
    var sb = new StringBuilder("<html><body>")
      .AppendLine("<h1>Aine Inventory Api</h1>")
      .AppendLine("<h3>Version 1.0</h3>")
      .AppendLine("<a href='swagger/index.html'>Swagger</a>")
      .AppendLine("</body></html>");
    return SendStringAsync(sb.ToString(), contentType: "text/html");
  }
}
