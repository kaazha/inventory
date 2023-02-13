using System.ComponentModel.DataAnnotations;
using Aine.Inventory.Core.CategoryAggregate;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

public class CreateCategoryRequest : ICategory
{
  [Required]
  public string? Name { get; set; }
  public string? Description { get; set; }
  public List<CreateSubCategoryRequest> SubCategories { get; set; } = default!;
  int ICategory.Id => 0;
  IEnumerable<ISubCategory> ICategory.SubCategories => SubCategories;
}

public class CreateSubCategoryRequest : ISubCategory
{
  int ISubCategory.Id => 0;
  public int CategoryId { get; set; }
  [Required]
  public string? Name { get; set; }
  public string? Description { get; set; }
}

public class UpdateSubCategoryRequest : ISubCategory
{
  public int Id { get; set; }
  public int CategoryId { get; set; }
  [Required]
  public string? Name { get; set; }
  public string? Description { get; set; }
}
