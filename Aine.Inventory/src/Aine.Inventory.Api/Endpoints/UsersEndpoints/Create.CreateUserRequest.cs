using System.ComponentModel.DataAnnotations;
using Aine.Inventory.SharedKernel.Security;
using Aine.Inventory.SharedKernel.Security.Interfaces;

namespace Aine.Inventory.Api.Endpoints.AuthEndpoint;

public class CreateUserRequest
{
  [Required]
  public string UserId { get; set; } = default!;
  public string UserFullName { get; set; } = default!;
  public string Password { get; set; } = default!;
  public ICollection<UserRoleInfo>? Roles {get; set;}
  public ICollection<UserPermissionInfo>? Permissions { get; set; }
}
