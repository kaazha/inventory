using System;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class GetProductNumbers : EndpointWithoutRequest<ICollection<ProductInfo>>
{
  private readonly IProductRepository _repository;

  public GetProductNumbers(IProductRepository repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/productNumbers");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var products = await _repository.GetProducts(cancellationToken);
    await SendOkAsync(products);
  }
}
