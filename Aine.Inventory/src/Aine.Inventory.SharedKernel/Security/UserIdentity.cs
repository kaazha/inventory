using System;
using Aine.Inventory.SharedKernel.Security.Interfaces;

namespace Aine.Inventory.SharedKernel.Security;

public class UserIdentity : IUser
{
  public int UserId { get; set; }
  public string UserName { get; set; } = default!;
  public string FullName { get; set; } = default!;
  public DateTime DateCreated { get; set; }
  public string? CreatedBy { get;  set; }
  public bool IsActive { get;  set; }
  public DateTime? LastUpdated { get; set; }
  public string? LastUpdatedBy { get; set; }
  public DateTime? LastLogIn { get; set; }

  public IEnumerable<UserRoleInfo>? Roles { get; set; }
  public IEnumerable<UserPermissionInfo>? Permissions { get; set; }

  public static UserIdentity Create(
    int userId,
    string userName,
    string? fullName,
    IEnumerable<UserRoleInfo>? roles = default,
    IEnumerable<UserPermissionInfo>? permissions = default
    )
  {
    return new UserIdentity
    {
      UserId = userId,
      UserName = userName,
      FullName = fullName ?? userName,
      Roles = roles,
      Permissions = permissions
    };
  }
}
