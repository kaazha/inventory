using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.CategoryAggregate;

public class Category : EntityBase<int>, IAggregateRoot
{
  public Category() { }

  public Category(string name, string? description)
  {
    Name = name;
    Description = description;
  }

  public string Name { get; set; } = default!;
  public string? Description { get; set; }
}

