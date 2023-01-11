using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Guard;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.ProductModelAggregate;

public class ProductModel : EntityBase<int>, IAggregateRoot
{
  public const int MAX_NAME_LENGTH = 50;

  private ProductModel() { }

  public ProductModel(string? name, string? description)
  {
    Validate(name, description);
    Name = name!;
    Description = description;
  }

  public string Name { get; private set; } = default!;
  public string? Description { get; private set; } = default!;

  internal static void Validate(string? name, string? description)
  {
    GuardModel.Against.NullOrEmpty(name, "Product Model Name can't be empty!");
    GuardModel.Against.TooLong(name!, MAX_NAME_LENGTH, $"Product Model Name can't exceed {MAX_NAME_LENGTH} characters!");
    GuardModel.Against.NullOrEmpty(description, "Product Model Description can't be empty!");
  }
}

