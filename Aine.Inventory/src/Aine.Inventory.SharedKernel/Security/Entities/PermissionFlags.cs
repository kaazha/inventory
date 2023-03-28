namespace Aine.Inventory.SharedKernel.Security;

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
