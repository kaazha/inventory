
using Aine.Inventory.Core.ProductAggregate;

namespace Aine.Inventory.Web.Endpoints.ProductEndpoints;

public class CategoryListResponse
{
  public List<CategoryRecord> Categories { get; set; } = new();
}

public record CategoryRecord(int Id, string Name, string Description);

