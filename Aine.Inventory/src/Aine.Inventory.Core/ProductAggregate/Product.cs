using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.ProductAggregate;

public class Product : EntityBase<Guid>, IAggregateRoot
{
  public string ProductNumber { get; set; } = default!;
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  public int CategoryId { get; set; }
  public Category? Category { get; set; } = null;
}

