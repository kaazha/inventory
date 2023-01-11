using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Guard;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.ProductInventoryAggregate;

public class Location : EntityBase<int>, IAggregateRoot
{
  public Location() { }

  public Location(string? name)
  {
    GuardModel.Against.NullOrEmpty(name, "Location Name can't be empty!");
    Name = name!;
  }

  public string Name { get; private set; } = default!;
}

