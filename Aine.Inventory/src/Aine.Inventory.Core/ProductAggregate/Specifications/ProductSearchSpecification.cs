using System.Linq.Expressions;
using Aine.Inventory.SharedKernel;
using Ardalis.Specification;

namespace Aine.Inventory.Core.ProductAggregate.Specifications;

public class ProductSearchSpecification : Specification<Product>
{
  public ProductSearchSpecification(IProductSearchParams @params)
  {
    Expression<Func<Product, bool>> predicate = p => p.IsActive == true;
    if (@params?.CategoryId > 0) predicate = predicate.AndAlso(p => p.SubCategory!.CategoryId == @params.CategoryId!);
    if (@params?.SubCategoryId > 0) predicate = predicate.AndAlso(p => p.SubCategoryId == @params.SubCategoryId!);
    if (!string.IsNullOrEmpty(@params?.Filter))
      predicate = predicate.AndAlso(p => p.Name.Contains(@params.Filter!) || p.ProductNumber.Contains(@params.Filter!));

    Query
      .Where(predicate)
      .Include(p => p.Model)
      .Include(p => p.SubCategory)
      //.Include(p => p.ProductPhoto)
      .AsSplitQuery();
  }
}
