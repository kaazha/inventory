using Ardalis.Specification;

namespace Aine.Inventory.Core.CategoryAggregate.Specifications;

public class AllCategoriesWithSubCategoriesSpecification : Specification<ProductCategory>
{
  public AllCategoriesWithSubCategoriesSpecification()
  {
    Query
      .Include(c => c.SubCategories.OrderBy(c => c.Name))
      .OrderBy(c => c.Name);
  }
}
