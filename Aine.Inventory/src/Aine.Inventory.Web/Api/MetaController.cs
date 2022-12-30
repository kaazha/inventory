using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Aine.Inventory.Web.Api;

public class MetaController : BaseApiController
{
  /// <summary>
  /// A sample API Controller. Consider using API Endpoints (see Endpoints folder) for a more SOLID approach to building APIs
  /// https://github.com/ardalis/ApiEndpoints
  /// </summary>
  [HttpGet("/info")]
  public ActionResult<string> Info()
  {
    var assembly = System.Reflection.Assembly.GetExecutingAssembly();

    var creationDate = System.IO.File.GetCreationTime(assembly.Location);
    var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

    return Ok($"Version: {version}, Last Updated: {creationDate}");
  }
}

