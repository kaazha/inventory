using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.ProductAggregate;

public class Category : EntityBase<int>, IAggregateRoot
{
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  //public int? ParentId { get; set; }
}

