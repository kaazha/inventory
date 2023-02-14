using System.ComponentModel.DataAnnotations;
using Aine.Inventory.Core.ProductInventoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.LocationEndpoints;

public class DeleteLocation : EndpointWithoutRequest
{ 
  private readonly IRepository<Location> _repository;

  public DeleteLocation(IRepository<Location> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Delete("/locations/{locationId}");
    AllowAnonymous();
    DontCatchExceptions();
  }

  [SwaggerOperation(
    Summary = "Deletes a Location",
    Description = "Deletes a specified Location",
    OperationId = "Location.Delete",
    Tags = new[] { "LocationEndpoints" })
  ]
  public override async Task HandleAsync(
    CancellationToken cancellationToken)
  {
    var id = Route<int>("locationId", true);
    var deleted = await _repository.DeleteByIdAsync(id, cancellationToken);
    if (deleted <= 0) await SendNotFoundAsync(cancellationToken);
    await SendOkAsync(cancellationToken);
  }
}
