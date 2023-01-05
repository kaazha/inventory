using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class List : EndpointWithoutRequest<ICollection<Product>>
{
  private readonly IReadRepository<Product> _repository;

  public List(IReadRepository<Product> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/products");
    AllowAnonymous();
  }

  public override async Task<ICollection<Product>> ExecuteAsync(
    CancellationToken cancellationToken)
  {
    var products = await _repository.ListAsync(cancellationToken);
    return products;
  }
}

