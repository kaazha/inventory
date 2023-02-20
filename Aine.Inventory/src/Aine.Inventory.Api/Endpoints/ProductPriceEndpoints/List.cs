using System;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductPriceAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.ProductPriceEndpoints;

public class List : EndpointWithoutRequest<IEnumerable<IProductPrice>> // ListProductPricesRequest
{
  private readonly IReadRepository<ProductPrice> _repository;

  public List(IReadRepository<ProductPrice> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/prices", "/prices/{productId}");
    AllowAnonymous();
  }

  public override async Task<IEnumerable<IProductPrice>> ExecuteAsync(
   //ListProductPricesRequest request,
   CancellationToken cancellationToken)
  {
    var productId = Query<int>("productId");
    var specification = new ProductPriceSpecification(productId);
    var prices = await _repository.ListAsync(specification, cancellationToken);
    return prices;
  }
}

public class ListProductPricesRequest
{
  public int ProductId { get; set; }
}
