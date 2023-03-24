
namespace Aine.Inventory.SharedKernel.Interfaces;

public interface IUser
{
  int UserId { get; }
  string UserName { get; }
  string? CorpName { get; }
  string FullName { get; }
  UserPrivileges Privileges { get; }
}

public class UserPrivileges
{
  public ICollection<string>? Roles { get; private set; }
  public ICollection<string>? Permissions { get; private set; }

  public static UserPrivileges Create(ICollection<string>? roles, ICollection<string>? permissions)
  {
    return new() { Permissions = permissions, Roles = roles };
  }
}