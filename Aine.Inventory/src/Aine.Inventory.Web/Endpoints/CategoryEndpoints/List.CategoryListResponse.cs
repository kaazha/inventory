
namespace Aine.Inventory.Web.Endpoints.CategoryEndpoints;

public class CategoryListResponse
{
  public List<CategoryRecord> Categories { get; set; } = new();
}

public record CategoryRecord(int Id, string Name, string? Description);

