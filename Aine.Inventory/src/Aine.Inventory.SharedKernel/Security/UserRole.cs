using System;
namespace Aine.Inventory.SharedKernel.Security;

public class UserRole
{
  public int UserId { get; set; }
  public Role Role { get; set; } = default!;
  public int RoleId { get; set; }
}