
using System.Linq.Expressions;
using Aine.Inventory.SharedKernel;
using Ardalis.Specification;

namespace Aine.Inventory.Core.ProductInventoryAggregate;

public class ProductInventorySpecification : Specification<ProductInventory>
{
  public ProductInventorySpecification(int? productId, int? locationId)
  {
    Expression<Func<ProductInventory, bool>> predicate = p => true;
    if (productId > 0) predicate = predicate.AndAlso(p => p.ProductId == productId);
    if (locationId > 0) predicate = predicate.AndAlso(p => p.LocationId == locationId);

    Query
      .Where(predicate)
      .Include(p => p.Location);
  }
}
