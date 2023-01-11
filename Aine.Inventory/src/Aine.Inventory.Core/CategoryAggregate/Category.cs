using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Guard;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.CategoryAggregate;

public class ProductCategory : EntityBase<int>, IAggregateRoot
{
  public const int MAX_NAME_LENGTH = 50;

  private ProductCategory() { }

  public ProductCategory(string? name, string? description)
  {
    Validate(name);
    Name = name!;
    Description = description;
  }

  public string Name { get; private set; } = default!;
  public string? Description { get; private set; }
  public ICollection<ProductSubCategory> SubCategories { get; private set; } = default!;

  internal static void Validate(string? name)
  {
    GuardModel.Against.NullOrEmpty(name, "Category Name can't be empty!");
    GuardModel.Against.TooLong(name!, MAX_NAME_LENGTH, $"Category Name can't exceed {MAX_NAME_LENGTH} characters!");
  }
}

