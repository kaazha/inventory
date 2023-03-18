namespace Aine.Inventory.Core.Models;

public class CategoryAndProductSummary
{
  public int TotalCategories { get; set; }
  public int TotalProducts { get; set; }
  public IEnumerable<ProductCountByCategory>? TotalProductsByCategory { get; set; }
}

public record ProductCountByCategory(string CategoryName, int TotalProducts);