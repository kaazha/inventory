using System;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductPriceAggregate;
using Aine.Inventory.Core.ProductPriceAggregate.DomainEvents;
using Aine.Inventory.SharedKernel.Interfaces;
using MediatR;

namespace Aine.Inventory.Core.DomainEventHandlers;

public class PriceUpdatedDomainEventHandler : INotificationHandler<PriceUpdatedDomainEvent>
{
  private readonly IProductRepository _productRepository;
  private readonly IReadRepository<ProductPrice> _priceRepository;
  private readonly ILogger _logger;

  public PriceUpdatedDomainEventHandler(
    IProductRepository productRepository,
    IReadRepository<ProductPrice> priceRepository,
    ILogger logger
     )
  {
    _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    _priceRepository = priceRepository ?? throw new ArgumentNullException(nameof(priceRepository));
    _logger = logger;
  }

  public async Task Handle(PriceUpdatedDomainEvent priceUpdatedEvent, CancellationToken cancellationToken)
  {
    var productId = priceUpdatedEvent.ProductId;
    var spec = new ProductPriceSpecification(productId, active: true);
    var activePrice = await _priceRepository.FirstOrDefaultAsync(spec);

    if(activePrice is null)
    {
      _logger.Warn($"PriceUpdatedDomainEventHandler: Product price data not found! (ProductId={productId})");
      return;
    }

    await _productRepository.UpdateProductListPrice(productId, activePrice.ListPrice, priceUpdatedEvent.User);
    _logger.Info($"PriceUpdatedDomainEventHandler: Successfully updated Product ListPrice for (ProductId={productId}, ListPrice={activePrice.ListPrice})");
  }
}
