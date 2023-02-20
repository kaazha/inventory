using Ardalis.Specification;

namespace Aine.Inventory.Core.ProductAggregate.Specifications;
public class ProductByIdSpecification : ProductSpecification
{
  public ProductByIdSpecification(int productId)
  {
    UpdateQuery(p => p.Id == productId);
    Query.Include(p => p.Inventory)
          .ThenInclude(p => p.Location)
          //.Include(p => p.Prices);
          ;
  }
}
