using System;
using System.Collections;
using Aine.Inventory.Core;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.SettingsEndpoints;

public class Get : EndpointWithoutRequest<IDictionary>
{
  private readonly ISettings _settings;

  public Get(ISettings settings)
  {
    _settings = settings;
  }

  public override void Configure()
  {
    Get("/settings");
    AllowAnonymous();
  }

  public override Task<IDictionary> ExecuteAsync(CancellationToken cancellationToken)
  {
    return Task.FromResult((IDictionary)_settings.All);
  }
}
