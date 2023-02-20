using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductInventoryAggregate;
using Aine.Inventory.Core.ProductPriceAggregate;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class UpdateProductRequest : IProduct
{
  public int Id { get; set; }
  public string ProductNumber { get; set; } = default!;
  public string Name { get; set; } = default!;
  public string? Description { get; set; }
  public int? SubCategoryId { get; set; }
  public int? ModelId { get; set; }
  public string? Color { get; set; }
  public string? Size { get; set; }
  public string? SizeUnit { get; set; }
  public double? Weight { get; set; }
  public string? WeightUnit { get; set; }
  public string? Style { get; set; }
  public int? ReorderPoint { get; set; }
  public double? StandardCost { get; set; }
  public double? ListPrice { get; set; }
  public bool IsActive { get; set; }
  public ICollection<InventoryModel>? Inventory { get; set; }
  public List<ProductPrice>? Prices { get; set; }
  IEnumerable<IInventory>? IProduct.Inventory => this.Inventory;
  IEnumerable<IProductPrice>? IProduct.Prices => this.Prices;
}