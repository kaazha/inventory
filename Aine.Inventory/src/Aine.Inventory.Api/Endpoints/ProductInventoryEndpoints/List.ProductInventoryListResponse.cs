using Aine.Inventory.Core.ProductInventoryAggregate;

namespace Aine.Inventory.Api.Endpoints.ProductInventoryEndpoints;

public class ProductInventoryListResponse
{
  public List<ProductInventory> Inventory { get; set; } = new();
}
