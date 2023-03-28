using System.ComponentModel.DataAnnotations;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductInventoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.LocationEndpoints;

[FastEndpoints.HttpPut("/locations")]
[AllowAnonymous]
public class Update : Endpoint<UpdateLocationRequest>
{
  private readonly IRepository<Location> _repository;

  public Update(IRepository<Location> repository)
  {
    _repository = repository;
  }

  public override async Task HandleAsync(
    UpdateLocationRequest request,
    CancellationToken cancellationToken)
  {
    var Location = new Location(request);
    await _repository.UpdateAsync(Location, cancellationToken);
    await SendOkAsync(request);
  }
}

public class UpdateLocationRequest : ILocation
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; } = default!;
}