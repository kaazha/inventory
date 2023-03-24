using System;
namespace Aine.Inventory.SharedKernel.Security;

public class Permission : EntityBase<int>
{
  public string PermissionTitle { get; set; } = default!;
  public string? Description { get; set; }
}