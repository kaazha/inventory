using System.Linq.Expressions;
using Ardalis.Specification;

namespace Aine.Inventory.Core.ProductAggregate.Specifications;

public class ProductSpecification : Specification<Product>
{
  public ProductSpecification() { }

  protected void UpdateQuery(Expression<Func<Product, bool>> predicate)
  {
    Query
      .Where(predicate)
      .Include(p => p.Model)
      .Include(p => p.SubCategory)
        .ThenInclude(c => c!.Category)
      .OrderBy(p => p.ProductNumber)
      .AsSplitQuery();
  }
}
