
using Aine.Inventory.SharedKernel.Security;

namespace Aine.Inventory.SharedKernel.Security.Interfaces;

public interface IUser
{
  int UserId { get; }
  string UserName { get; }
  string FullName { get; }

  IEnumerable<UserRoleInfo>? Roles { get; }
  IEnumerable<UserPermissionInfo>? Permissions { get; }
}
