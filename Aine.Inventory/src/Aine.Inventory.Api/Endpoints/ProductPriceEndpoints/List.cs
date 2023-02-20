using System;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductPriceAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.ProductPriceEndpoints;

public class List : Endpoint<ListProductPricesRequest, IEnumerable<IProductPrice>>
{
  private readonly IReadRepository<ProductPrice> _repository;

  public List(IReadRepository<ProductPrice> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/products/{productId}/prices");
    AllowAnonymous();
  }

  public override async Task<IEnumerable<IProductPrice>> ExecuteAsync(
   ListProductPricesRequest request,
   CancellationToken cancellationToken)
  {
    var specification = new ProductPriceSpecification(request.ProductId);
    var prices = await _repository.ListAsync(specification, cancellationToken);
    return prices;
  }
}

public class ListProductPricesRequest
{
  public int ProductId { get; set; }
}
