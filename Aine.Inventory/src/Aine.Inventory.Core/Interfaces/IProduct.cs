
namespace Aine.Inventory.Core.Interfaces;

public interface IProduct
{
  int Id { get; }
  string ProductNumber { get; }
  string Name { get; }
  string? Description { get; }
  int? SubCategoryId { get; }
  int? ModelId { get; }
  string? Color { get; }
  string? Size { get; }
  string? SizeUnit { get; }
  double? Weight { get; }
  string? WeightUnit { get; }
  string? Style { get; }
  int? ReorderPoint { get; }
  double? StandardCost { get; }
  double? ListPrice { get; }  
  bool IsActive { get; }  
}
