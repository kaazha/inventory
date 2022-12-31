using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.ProductAggregate;

public class Product : EntityBase<Guid>, IAggregateRoot
{
  public string ProductNumber { get; set; } = default!;
  public string Name { get; set; } = default!;
  public string? Description { get; set; }
  public int CategoryId { get; set; }
  public int SubCategoryId { get; set; }
  public Category? Category { get; set; }
  public Category? SubCategory { get; set; }
  public string? Model { get; set; } 
  public string? Color { get; set; }
  public string? Size { get; set; }
  public string? UnitOfMeasurement { get; set; }
  public float? Weight { get; set; }
  public string? Style { get; set; }
  public int? ReorderPoint { get; set; }
  public float? StandardCost { get; set; }
  public float? ListPrice { get; set; }
  public string? ProductPhoto { get; set; }
  public bool IsActive { get; set; } = true;
  public DateTime? ModifiedDate { get; set; }
}

