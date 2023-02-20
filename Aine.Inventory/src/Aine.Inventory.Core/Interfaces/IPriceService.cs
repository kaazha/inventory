using Aine.Inventory.Core.ProductPriceAggregate;
using Ardalis.Result;

namespace Aine.Inventory.Core.Interfaces;

public interface IPriceService
{
  Task<Result<ProductPrice>> CreatePriceAsync(IPriceChange price, CancellationToken cancellationToken);
  Task<Result<ProductPrice>> UpdatePriceAsync(IProductPrice price, CancellationToken cancellationToken);
}
