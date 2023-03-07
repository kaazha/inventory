using System;
using Aine.Inventory.SharedKernel;

namespace Aine.Inventory.Core.ProductPriceAggregate.DomainEvents;

public class PriceUpdatedDomainEvent : DomainEventBase
{
  public PriceUpdatedDomainEvent()
  {
  }

  public PriceUpdatedDomainEvent(int productId, object? user) => (ProductId, User) = (productId, user);

  public int ProductId { get; private set; }
  public object? User { get; private set; }
}

