
using Aine.Inventory.SharedKernel.Interfaces;
using Aine.Inventory.SharedKernel.Security.Interfaces;

namespace Aine.Inventory.SharedKernel.Security;

public class User : EntityBase<int>, IAggregateRoot, IUser
{
  public string UserName { get; private set; } = default!;
  public string FullName { get; private set; } = default!;
  public byte[]? Avatar { get; private set; }
  public string Password { get; private set; } = default!;
  public DateTime DateCreated { get; private set; }
  public string? CreatedBy { get; private set; }
  public bool IsActive { get; private set; }
  public DateTime? LastUpdated { get; private set; }
  public string? LastUpdatedBy { get; private set; }
  public DateTime? LastLogIn { get; private set; }

  public ICollection<UserRole>? Roles { get; private set; }
  public ICollection<UserPermission>? Permissions { get; private set; }

  int IUser.UserId => Id;
  IEnumerable<UserRoleInfo>? IUser.Roles =>
      Roles?.Select(r => new UserRoleInfo(r.RoleId, r.Role?.RoleName ?? string.Empty, r.Role?.IsAdminRole));

  IEnumerable<UserPermissionInfo>? IUser.Permissions =>
      Permissions?.Select(p => new UserPermissionInfo(p.PermissionId, p.Permission?.PermissionTitle ?? string.Empty, p.Permissions));

  public static User Create (
    int userId, 
    string userName,
    string fullName,
    string password,
    byte[]? avatar,
    string creator,
    ICollection<UserRoleInfo>? roles,
    ICollection<UserPermissionInfo>? permissions)
  {

    return new User
    {
      Id = userId,
      UserName = userName,
      FullName = fullName,
      DateCreated = DateTime.UtcNow,
      Avatar = avatar,
      IsActive = true,
      Password = password,
      CreatedBy = creator,
      Roles = roles?.Select(role => UserRole.Create(userId, role.RoleId, role.RoleName))?.ToList(),
      Permissions = permissions?.Select(p => UserPermission.Create(userId, p.PermissionId ?? 0, p.PermissionName, p.Permissions))?.ToList(),
    };
  }

  public void EncryptPassword(IEncryptor encryptor)
  {
    this.Password = encryptor.Encrypt(Password);
  }
}