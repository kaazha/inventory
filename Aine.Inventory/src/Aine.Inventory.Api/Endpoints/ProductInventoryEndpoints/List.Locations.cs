using Aine.Inventory.Core.ProductInventoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.ProductInventoryEndpoints;

public class GetLocations : EndpointWithoutRequest<IEnumerable<Location>>
{
  private readonly IReadRepository<Location> _repository;

  public GetLocations(IReadRepository<Location> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/locations1");
    AllowAnonymous();
  }

  public override async Task<IEnumerable<Location>> ExecuteAsync(CancellationToken cancellationToken)
  {
    return await _repository.ListAsync(cancellationToken);
  }
}