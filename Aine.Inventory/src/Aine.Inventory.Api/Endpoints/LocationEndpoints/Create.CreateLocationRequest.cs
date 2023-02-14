using System.ComponentModel.DataAnnotations;
using Aine.Inventory.Core.Interfaces;

namespace Aine.Inventory.Api.Endpoints.LocationEndpoints;

public class CreateLocationRequest : ILocation
{
  [Required]
  public string Name { get; set; } = default!;
  int ILocation.Id => 0;
}
