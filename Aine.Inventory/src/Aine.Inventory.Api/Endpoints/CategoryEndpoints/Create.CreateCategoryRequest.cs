using System.ComponentModel.DataAnnotations;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

public class CreateCategoryRequest
{
  [Required]
  public string? Name { get; set; }
  public string? Description { get; set; }
}
