
using Aine.Inventory.Core.CategoryAggregate;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

public class CategoryListResponse
{
  public List<CategoryRecord> Categories { get; set; } = new();
}

public record CategoryRecord(
  int Id,
  string Name,
  string? Description,
  ICollection<ProductSubCategory> SubCategories
);

