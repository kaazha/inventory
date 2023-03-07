using System.Linq.Expressions;
using Aine.Inventory.SharedKernel;
using Ardalis.Specification;

namespace Aine.Inventory.Core.ProductPriceAggregate;

public class ProductPriceSpecification : Specification<ProductPrice>
{
  public ProductPriceSpecification(int? productId, bool active = false)
  {
    Expression<Func<ProductPrice, bool>> predicate = p => true;
    if (productId > 0) predicate = predicate.AndAlso(p => p.ProductId == productId);
    //if(currentOnly) predicate = predicate.AndAlso(p => p.EndDate == null || );

    Query
      .Where(predicate)
      .OrderByDescending(p => p.EffectiveDate);
    if (active) Query.Take(1);
  }
}
