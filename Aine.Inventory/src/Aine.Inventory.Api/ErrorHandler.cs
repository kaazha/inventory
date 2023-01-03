using Aine.Inventory.SharedKernel;
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Aine.Inventory.Api;

public static class ErrorHandler
{
  public static async Task HandleException(HttpContext context)
  {
    var response = context.Response;

    // using static System.Net.Mime.MediaTypeNames;
    response.ContentType = Text.Plain;

    var exceptionHandlerPathFeature =
      context.Features.Get<IExceptionHandlerPathFeature>();
    var error = exceptionHandlerPathFeature?.Error;
    
    if (error is ModelValidationException)
    {
      response.StatusCode = StatusCodes.Status400BadRequest;
      await response.WriteAsync(error.Message);
      return;
    }

    response.StatusCode = StatusCodes.Status500InternalServerError;
    await response.WriteAsync("An unhandled error occurred during processing the request: ");

#if DEBUG
    await response.WriteAsync($": {error}");
#endif

    if (error is FileNotFoundException)
    {
      await response.WriteAsync(" The file was not found.");
    }

    if (exceptionHandlerPathFeature?.Path == "/")
    {
      await response.WriteAsync(" Page: Home.");
    }
  }
}
