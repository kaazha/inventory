using Aine.Inventory.Core.ProductAggregate;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class ProductListResponse
{
  public List<Product> Products { get; set; } = new();
}
