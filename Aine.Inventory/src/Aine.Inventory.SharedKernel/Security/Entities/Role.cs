using System;
using Aine.Inventory.SharedKernel.Security.Interfaces;

namespace Aine.Inventory.SharedKernel.Security;

public class Role : EntityBase<int>, ISecurityObject
{
  public const string ADMIN_ROLE = "A";

  public string RoleName { get; private set; } = default!;
  public string? Description { get; private set; }
  public bool IsAdminRole { get; private set; }

  int ISecurityObject.Id { get => Id; set => Id = value; }
  string? ISecurityObject.Name => RoleName;
}