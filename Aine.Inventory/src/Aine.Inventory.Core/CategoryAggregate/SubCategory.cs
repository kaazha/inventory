using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.CategoryAggregate;

public class ProductSubCategory : EntityBase<int>, IAggregateRoot, ISubCategory
{
  private ProductSubCategory() { }

  public ProductSubCategory(ISubCategory subCategory) : this(
    subCategory.Id,
    subCategory.Name,
    subCategory.CategoryId,
    subCategory.Description
    ) { }

  public ProductSubCategory(int id, string? name, int categoryId, string? description)
  {
    ProductCategory.Validate(name);

    Id = id;
    Name = name!;
    Description = description;
    CategoryId = categoryId;
  }

  public int CategoryId { get; private set; } = default;
  public ProductCategory? Category { get; private set; }
  public string Name { get; private set; } = default!;
  public string? Description { get; private set; }
}
