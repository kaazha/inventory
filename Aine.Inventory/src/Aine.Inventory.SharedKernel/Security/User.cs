
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.SharedKernel.Security;

public class User : EntityBase<int>
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

  public ICollection<UserRole>? UserRoles { get; private set; }
  public ICollection<UserPermission>? Permissions { get; private set; }

  public static User Create (
    int userId, 
    string userName,
    string fullName,
    string password,
    byte[]? avatar,
    string creator)
  {
    return new User
    {
      Id = userId,
      UserName = userName,
      FullName = fullName,
      DateCreated = DateTime.UtcNow,
      Avatar = avatar, IsActive = true,
      Password = password,
      CreatedBy = creator
    };
  }
}