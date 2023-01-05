using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Mapster;
using MediatR;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class List : EndpointWithoutRequest<IEnumerable<ProductDto>>
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

  public override async Task<IEnumerable<ProductDto>> ExecuteAsync(
    CancellationToken cancellationToken)
  {
    var specification = new ProductSearchSpecification();
    var products = await _repository.ListAsync(specification, cancellationToken);
    return products.Map();
  }
}

