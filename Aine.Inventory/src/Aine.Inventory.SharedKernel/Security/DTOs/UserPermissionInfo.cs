namespace Aine.Inventory.SharedKernel.Security;

using static AllowOrDeny;

public class UserPermissionInfo
{
  public UserPermissionInfo() { }

  public UserPermissionInfo(int permissionId, string permissionTitle, PermissionFlags permissionFlag)
  {
    PermissionId = permissionId;
    PermissionName = permissionTitle;
    All = permissionFlag == PermissionFlags.Allow || permissionFlag == PermissionFlags.AllowAllCrud ? Allow : null;
    Create = permissionFlag.HasFlag(PermissionFlags.AllowCreate) ? Allow : null;
    Update = permissionFlag.HasFlag(PermissionFlags.AllowUpdate) ? Allow : null;
    Delete = permissionFlag.HasFlag(PermissionFlags.AllowDelete) ? Allow : null;
    Read = permissionFlag.HasFlag(PermissionFlags.AllowView) ? Allow : null;
  }

  public int? PermissionId { get; set; }
  public string? PermissionName { get; set; } = default!;
  public AllowOrDeny? All { get; set; }
  public AllowOrDeny? Create { get; set; }
  public AllowOrDeny? Update { get; set; }
  public AllowOrDeny? Read { get; set; }
  public AllowOrDeny? Delete { get; set; }

  public PermissionFlags Permissions
  {
    get
    {
      var flags = PermissionFlags.Deny;
      if (All == Allow) flags |= PermissionFlags.Allow;
      if (Create == Allow) flags |= PermissionFlags.AllowCreate;
      if (Update == Allow) flags |= PermissionFlags.AllowUpdate;
      if (Delete == Allow) flags |= PermissionFlags.AllowDelete;
      if (Read == Allow) flags |= PermissionFlags.AllowView;
      return flags;
    }
  }
}
