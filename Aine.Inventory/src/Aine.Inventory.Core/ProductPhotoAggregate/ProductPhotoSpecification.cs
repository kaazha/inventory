
using Ardalis.Specification;

namespace Aine.Inventory.Core.ProductPhotoAggregate;
public class ProductPhotoSpecification : Specification<ProductPhoto>
{
  public ProductPhotoSpecification(int productId)
  {
    Query
      .Where(p => p.ProductId == productId);
  }
}
