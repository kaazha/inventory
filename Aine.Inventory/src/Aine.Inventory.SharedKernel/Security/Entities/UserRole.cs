using System;
using Aine.Inventory.SharedKernel.Security.Interfaces;

namespace Aine.Inventory.SharedKernel.Security;

public class UserRole : ISecurityObject
{
  private string? _roleName;

  public int UserId { get; private set; }
  public Role Role { get; private set; } = default!;
  public int RoleId { get; set; }

  public string? RoleName
  {
    get => _roleName ?? Role?.RoleName;
    set => _roleName = value;
  }

  int ISecurityObject.Id { get => RoleId; set => RoleId = value; }
  string? ISecurityObject.Name => RoleName;

  public static UserRole Create(int userId, int roleId, string? roleName) =>
    new()
    {
      UserId = userId,
      RoleId = roleId,
      _roleName = roleName
    };
}
