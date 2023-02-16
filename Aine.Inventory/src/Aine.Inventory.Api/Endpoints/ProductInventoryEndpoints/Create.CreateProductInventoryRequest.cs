
using Aine.Inventory.Core.Interfaces;

namespace Aine.Inventory.Api.Endpoints.ProductInventoryEndpoints;

public class CreateProductInventoryRequest : IInventory
{
  public int Id { get; }
  public int LocationId { get; set; }
  public int ProductId { get; set; }
  public string? Shelf { get; set; }
  public string? Bin { get; set; }
  public int Quantity { get; set; }
}
