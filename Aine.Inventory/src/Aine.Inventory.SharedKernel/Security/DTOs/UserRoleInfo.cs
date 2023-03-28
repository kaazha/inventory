namespace Aine.Inventory.SharedKernel.Security;

public class UserRoleInfo
{
  public UserRoleInfo() { }

  public UserRoleInfo(int roleId, string roleName, bool? isAdminRole)
    => (RoleId, RoleName, RoleType) = (roleId, roleName, isAdminRole == true ? Role.ADMIN_ROLE : null);

  public int RoleId { get; set; }
  public string RoleName { get; set; } = default!;
  public string? RoleType { get; }
}
