using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Guard;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.ProductInventoryAggregate;

public class Location : EntityBase<int>, IAggregateRoot, ILocation
{
  public Location() { }

  public Location(ILocation location) :this(location.Id, location.Name) { }

  public Location(int id, string? name)
  {
    GuardModel.Against.NullOrEmpty(name, "Location Name can't be empty!");
    Id = id;
    Name = name!;
  }

  public string Name { get; private set; } = default!;
}
