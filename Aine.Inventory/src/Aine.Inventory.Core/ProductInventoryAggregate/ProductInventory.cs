using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Guard;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.ProductInventoryAggregate;

public class ProductInventory : EntityBase<int>, IAggregateRoot, IInventory
{
  public ProductInventory(int id, int productId, int locationId, string? shelf, string? bin, int quantity = 0)
  {
    GuardModel.Against.Negative(productId, "Invalid product");
    GuardModel.Against.ZeroOrNegative(locationId, "Invalid inventory location");
    GuardModel.Against.Negative(quantity, $"Invalid inventory quantity ({quantity})");

    Id = id;
    ProductId = productId;
    LocationId = locationId;
    Shelf = shelf;
    Bin = bin;
    Quantity = quantity;
    ModifiedDate = DateTime.UtcNow;
  }

  public ProductInventory() { }

  public int LocationId { get; private set; }
  public Location? Location { get; internal set; }
  public int ProductId { get; internal set; }
  public string? Shelf { get; private set; }
  public string? Bin { get; private set; }
  public int Quantity { get; private set; }
  public DateTime? ModifiedDate { get; private set; }

  public static ProductInventory Create(IInventory inventory, int? productId = default)
  {
    return new ProductInventory(
      inventory.Id,
      productId ?? inventory.ProductId,
      inventory.LocationId,
      inventory.Shelf,
      inventory.Bin,
      inventory.Quantity
      );
  }
}
