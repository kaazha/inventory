using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class Search : Endpoint<ProductListRequest, IEnumerable<ProductDto>>
{
  private readonly IReadRepository<Product> _repository;

  public Search(IReadRepository<Product> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/products/search");
    AllowAnonymous();
  }

  public override async Task<IEnumerable<ProductDto>> ExecuteAsync(ProductListRequest request,
    CancellationToken cancellationToken)
  {
    var specification = new ProductSearchSpecification(request);
    var products = await _repository.ListAsync(specification, cancellationToken);
    return products.Map();
  }
}

