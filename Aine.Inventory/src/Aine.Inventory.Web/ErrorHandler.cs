using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Aine.Inventory.Web;

public static class ErrorHandler
{
  public static async Task HandleException(HttpContext context)
  {
    var response = context.Response;

    response.StatusCode = StatusCodes.Status500InternalServerError;

    // using static System.Net.Mime.MediaTypeNames;
    response.ContentType = Text.Plain;

    await response.WriteAsync("An exception was thrown.");

    var exceptionHandlerPathFeature =
      context.Features.Get<IExceptionHandlerPathFeature>();

    if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
    {
      await response.WriteAsync(" The file was not found.");
    }

    if (exceptionHandlerPathFeature?.Path == "/")
    {
      await response.WriteAsync(" Page: Home.");
    }
  }
}
