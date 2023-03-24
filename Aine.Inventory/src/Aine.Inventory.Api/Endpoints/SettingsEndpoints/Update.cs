using System;
using Aine.Inventory.Core;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.SettingsEndpoints;

public class Update : Endpoint<SettingUpdateRequest>
{
  private readonly ISettings _settings;

  public Update(ISettings settings)
  {
    _settings = settings;
  }

  public override void Configure()
  {
    Put("/settings");
  }

  public override async Task HandleAsync(SettingUpdateRequest request, CancellationToken cancellationToken)
  {
    await _settings.Update(request, cancellationToken);
    await SendOkAsync(cancellationToken);
  }
}

public class SettingUpdateRequest : Dictionary<string, string>
{
  
}