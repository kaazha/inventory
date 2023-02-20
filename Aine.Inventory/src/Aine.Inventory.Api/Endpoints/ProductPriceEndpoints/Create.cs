using System;
using Aine.Inventory.Api.Endpoints.ProductInventoryEndpoints;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductPriceAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.ProductPriceEndpoints;

public class Create : Endpoint<CreateProductPriceRequest, IProductPrice>
{
  private readonly IRepository<ProductPrice> _repository;

  public Create(IRepository<ProductPrice> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post("/products/prices");
    AllowAnonymous();
  }

 public override async Task HandleAsync(
    CreateProductPriceRequest request,
    CancellationToken cancellationToken)
  {
    if(request.ProductId <= 0)
    {
      await SendStringAsync("Invalid or unspecified ProductId!", StatusCodes.Status400BadRequest);
      return;
    }

    var currentPrice = await _repository.FirstOrDefaultAsync(new ProductPriceSpecification(request.ProductId, true), cancellationToken);
    var priceChange = currentPrice != null ? request.ListPrice - currentPrice.ListPrice : (double?) null;
    var priceInfo = ProductPrice.Create(
      id: 0,
      request.ProductId,
      request.EffectiveDate,
      request.EndDate,
      request.ListPrice,
      priceChange,
      User?.Identity?.Name,
      DateTime.UtcNow,
      request.Notes
      );

    if(currentPrice is not null)
    {
      currentPrice!.SetEndDate();
      await _repository.UpdateAsync(currentPrice, cancellationToken);
    }

    var createdItem = await _repository.AddAsync(priceInfo, cancellationToken);
    await SendAsync(createdItem, StatusCodes.Status201Created);
  }
}
