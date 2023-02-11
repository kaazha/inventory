using System.ComponentModel.DataAnnotations;
using Aine.Inventory.Core.CategoryAggregate;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

public class UpdateCategoryRequest : ICategory
{
  public int Id { get; set; }
  [Required]
  public string? Name { get; set; }
  public string? Description { get; set; }
  public IEnumerable<SubCategory> SubCategories { get; set; } = default!;

  IEnumerable<ISubCategory> ICategory.SubCategories => SubCategories;
}
