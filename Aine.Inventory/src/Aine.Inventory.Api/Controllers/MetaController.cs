using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Controllers;

public class MetaController : BaseApiController
{
  [HttpGet("/")]
  [SwaggerOperation(Tags = new[] { "Index" })]
  public IActionResult Home()
  {
    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
    var creationDate = System.IO.File.GetCreationTime(assembly.Location);
    var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
    var assemblies = new StringBuilder();
    foreach(var a in AppDomain.CurrentDomain.GetAssemblies())
      assemblies.AppendLine("<li>" + a.FullName + "</li>");

    var sb = new StringBuilder("<html><body>")
     .AppendLine("<h1>Aine Inventory Api</h1>")
     .AppendLine($"<h3>Version: {version}</h3>")
     .AppendLine($"<h4>Last Updated: {creationDate}</h4>")
     .AppendLine($"<a href='{this.Request.PathBase}/swagger/index.html'>Swagger</a>")
     .AppendLine("<h4>Assemblies: <h4>")
     .AppendLine("<ul>" + assemblies.ToString() + "</ul>")
     .AppendLine("</body></html>");
    return Content(sb.ToString(), contentType: "text/html");
  }

  /// <summary>
  /// A sample API Controller. Consider using API Endpoints (see Endpoints folder) for a more SOLID approach to building APIs
  /// https://github.com/ardalis/ApiEndpoints
  /// </summary>
  [HttpGet("/info")]
  [SwaggerOperation(Tags = new[] { "Index" })]
  public ActionResult<string> Info()
  {
    var assembly = System.Reflection.Assembly.GetExecutingAssembly();

    var creationDate = System.IO.File.GetCreationTime(assembly.Location);
    var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

    return Ok($"Version: {version}, Last Updated: {creationDate}");
  }
}

