using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.Core.CategoryAggregate.Specifications;
using Aine.Inventory.Core.ProductInventoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.LocationEndpoints;

public class List : EndpointWithoutRequest<ICollection<LocationRecord>>
{
  private readonly IReadRepository<Location> _repository;

  public List(IReadRepository<Location> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/locations");
    AllowAnonymous();
  }

  public override async Task<ICollection<LocationRecord>> ExecuteAsync(
    CancellationToken cancellationToken)
  {
    var Locations = await _repository.ListAsync(cancellationToken);
    return Locations
        .OrderBy(x => x.Name)
        .Select(c => new LocationRecord(c.Id, c.Name))
        .ToList();
  }
}

