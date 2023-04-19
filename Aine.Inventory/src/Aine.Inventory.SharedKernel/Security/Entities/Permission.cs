using Aine.Inventory.SharedKernel.Security.Interfaces;

namespace Aine.Inventory.SharedKernel.Security;

public class Permission : EntityBase<int>, ISecurityObject
{
  public string PermissionTitle { get; private set; } = default!;
  public string? Description { get; private  set; }
  public PermissionType? PermissionType { get; private set; }

  int ISecurityObject.Id { get => Id; set => Id = value; }
  string? ISecurityObject.Name => PermissionTitle;
}

public enum PermissionType { Object, Other }