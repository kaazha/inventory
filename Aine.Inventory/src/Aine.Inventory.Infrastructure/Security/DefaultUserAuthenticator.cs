using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Security;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using Aine.Inventory.SharedKernel.Security.Specifications;
using Ardalis.Result;
using static Aine.Inventory.SharedKernel.Security.AllowOrDeny;

namespace Aine.Inventory.Infrastructure.Security;

[Inject]
public class DefaultUserAuthenticator  : IUserAuthenticator
{
  private readonly IEncryptor _encryptor;
  private readonly IUserService _userService;

  public DefaultUserAuthenticator(IEncryptor encryptor, IUserService userService)
  {
    _encryptor = encryptor;
    _userService = userService;
  }

  public async Task<Result<IUser>> AuthenticateUserAsync(AuthenticationRequest auth)
  {
    ArgumentNullException.ThrowIfNull(auth, nameof(auth));
    if (string.IsNullOrEmpty(auth.UserName))
      return Result.Error("User Name can't be empty.");
    if (string.IsNullOrEmpty(auth.Password))
      return Result.Error("Password can't be empty.");

    var password = _encryptor.Encrypt(auth.Password);
    var specification = new ActiveUserByUserNameAndPasswordSpecification(auth.UserName, password, auth.Password);
    var user = await _userService.FindUserAsync(specification);
    if(user == null)
      return Result.Error("Invalid credentials.");

    await SetAdminPermissions(user);
    ExtendPermissions(user);

    return user;
  }

  private async Task SetAdminPermissions(UserIdentity user)
  {
    if (user.Roles == null || !user.Roles.Any(r => r.RoleType == Role.ADMIN_ROLE)) return;

    var allPermissions = await _userService.GetPermissionsAsync();
    var userPermissions = new List<UserPermissionInfo>(user.Permissions ?? Array.Empty<UserPermissionInfo>());
    var existingPermissionIds = userPermissions.Select(p => p.PermissionId).ToHashSet();
    allPermissions.Where(p => !existingPermissionIds.Contains(p.Id))
                          .ForEachItem(p => userPermissions.Add(
                                new UserPermissionInfo(p.Id, p.PermissionTitle, PermissionFlags.Allow | PermissionFlags.AllowAllCrud)
                         ));
    user.Permissions = userPermissions;
  }

  private static void ExtendPermissions(UserIdentity user)
  {
    if (user.Permissions == null || !user.Permissions.Any()) return;
    var permissions = new List<UserPermissionInfo>();
    foreach(var permission in user.Permissions)
    {
      if(permission.All == Allow) permissions.Add(permission);
      AddPermission(permission, "Create", permission.Create == Allow);
      AddPermission(permission, "Update", permission.Update == Allow);
      AddPermission(permission, "Delete", permission.Delete == Allow);
      AddPermission(permission, "View", permission.Read == Allow);
    }

    user.Permissions = permissions;

    void AddPermission(UserPermissionInfo permission, string action, bool add)
    {
      if (!add) return;
      if (!string.IsNullOrEmpty(action)) action += " ";
      permissions.Add(new UserPermissionInfo(permission.PermissionId ?? 0, $"{action}{permission.PermissionName}", PermissionFlags.Allow));
    }
  }
}
