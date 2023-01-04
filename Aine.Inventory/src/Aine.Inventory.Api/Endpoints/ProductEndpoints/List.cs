using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class Get : Endpoint<ProductListRequest, ProductListResponse>
{
  private readonly IReadRepository<Product> _repository;

  public Get(IReadRepository<Product> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/products");
    AllowAnonymous();
  }

  public override async Task<ProductListResponse> ExecuteAsync(ProductListRequest request,
    CancellationToken cancellationToken)
  {
    var specification = new ProductSearchSpecification(request);
    var products = await _repository.ListAsync(specification, cancellationToken);
    var response = new ProductListResponse { Products = products };
    return response;
  }
}

