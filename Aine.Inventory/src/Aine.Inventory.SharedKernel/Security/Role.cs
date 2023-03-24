using System;
namespace Aine.Inventory.SharedKernel.Security;

public class Role : EntityBase<int>
{
  public string RoleName { get; set; } = default!;
  public string? Description { get; set; }
}