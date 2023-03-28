using Aine.Inventory.SharedKernel.Security.Interfaces;

namespace Aine.Inventory.SharedKernel.Security;

public class UserPermission : ISecurityObject
{
  private string? _permissionName;

  public int UserId { get; private set; }
  public Permission Permission { get; private set; } = default!;
  public int PermissionId { get; private set; }
  public PermissionFlags Permissions { get; private set; }

  public string? PermissionName
  {
    get => _permissionName ?? Permission?.PermissionTitle;
    set => _permissionName = value;
  }

  int ISecurityObject.Id { get => PermissionId; set => PermissionId = value; }
  string? ISecurityObject.Name => PermissionName;

  public static UserPermission Create(int userId, int permissionId, string? permissionName, PermissionFlags permissions) =>
    new()
    {
      UserId = userId,
      PermissionId = permissionId,
      Permissions = permissions,
      _permissionName = permissionName
    };
}
