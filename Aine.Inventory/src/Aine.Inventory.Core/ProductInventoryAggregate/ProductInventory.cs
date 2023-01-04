
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.ProductInventoryAggregate;

public class ProductInventory : EntityBase<int>, IAggregateRoot
{
  public ProductInventory(int productId, int locationId, string? shelf, string? bin, int quantity = 0)
  {
    GuardModel.Against.ZeroOrNegative(productId, "Invalid product");
    GuardModel.Against.ZeroOrNegative(locationId, "Invalid inventory location");
    GuardModel.Against.Negative(quantity, $"Invalid inventory quantity ({quantity})");

    ProductId= productId;
    LocationId = locationId;
    Shelf = shelf;
    Bin = bin;
    Quantity = quantity;
  }

  private ProductInventory() { }

  public int LocationId { get; private set; }
  public Location? Location { get; private set; }
  public int ProductId { get; private set; }
  public string? Shelf { get; private set; }
  public string? Bin { get; private set; }
  public int Quantity { get; private set; }
  public DateTime? ModifiedDate { get; private set; }
}
