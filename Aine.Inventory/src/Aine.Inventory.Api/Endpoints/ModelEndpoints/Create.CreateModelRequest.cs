using System.ComponentModel.DataAnnotations;

namespace Aine.Inventory.Api.Endpoints.ModelEndpoints;

public class CreateModelRequest
{
  [Required]
  public string? Name { get; set; }
  public string? Description { get; set; }
}
