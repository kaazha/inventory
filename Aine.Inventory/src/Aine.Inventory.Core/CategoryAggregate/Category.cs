using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Guard;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.CategoryAggregate;

public class ProductCategory : EntityBase<int>, IAggregateRoot, ICategory
{
  public const int MAX_NAME_LENGTH = 50;

  private ProductCategory() { }

  public ProductCategory(ICategory other) : this(
    other.Id,
    other.Name,
    other.Description,
    other.SubCategories
    )
  { }

  public ProductCategory(
    string? name,
    string? description,
    IEnumerable<ISubCategory>? subCategories = default)
    : this(0, name, description, subCategories) { }

  public ProductCategory(
    int id,
    string? name,
    string? description,
    IEnumerable<ISubCategory>? subCategories = default)
  {
    Validate(name);
    Id = id;
    Name = name!;
    Description = description;
    if (subCategories is not null) SubCategories = subCategories.Select(sc => new ProductSubCategory(sc)).ToList();
  }

  public string Name { get; private set; } = default!;
  public string? Description { get; private set; }
  public ICollection<ProductSubCategory> SubCategories { get; private set; } = default!;
  IEnumerable<ISubCategory> ICategory.SubCategories => SubCategories;

  internal static void Validate(string? name)
  {
    GuardModel.Against.NullOrEmpty(name, "Category Name can't be empty!");
    GuardModel.Against.TooLong(name!, MAX_NAME_LENGTH, $"Category Name can't exceed {MAX_NAME_LENGTH} characters!");
  }
}

