using Aine.Inventory.Core.ProductInventoryAggregate;
using Aine.Inventory.Core.ProductModelAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.LocationEndpoints;

[HttpPost("/locations")]
[AllowAnonymous]
public class Create : Endpoint<CreateLocationRequest, Location>
{
  private readonly IRepository<Location> _repository;

  public Create(IRepository<Location> repository)
  {
    _repository = repository;
  }

  public override async Task HandleAsync(
    CreateLocationRequest request,
    CancellationToken cancellationToken)
  {
    var location = new Location(request);
    var createdItem = await _repository.AddAsync(location, cancellationToken);

    await SendAsync(createdItem, StatusCodes.Status201Created, cancellation: cancellationToken);
  }
}
