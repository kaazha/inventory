using System.ComponentModel.DataAnnotations;

namespace Aine.Inventory.Web.Endpoints.ProjectEndpoints;

public class CreateProjectRequest
{
  public const string Route = "/Projects";

  [Required]
  public string? Name { get; set; }
}

