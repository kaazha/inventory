using System;
namespace Aine.Inventory.SharedKernel.Security;

public class UserRole
{
  public int UserId { get; set; }
  public Role Role { get; set; } = default!;
  public int RoleId { get; set; }
}

public class UserPermission
{
  public UserPermission()
  {
  }

  public int UserId { get; set; }
  public Permission Permission { get; set; } = default!;
  public int PermissionId { get; set; }
  public PermissionFlags PermissionFlag { get; set; }
}

[Flags]
public enum PermissionFlags
{
  Deny,
  Allow,
  AllowCreate = 2,
  AllowUpdate = 4,
  AllowView = 8,
  AllowDelete = 16,
  AllowAllCrud = 30,
}