
namespace Aine.Inventory.SharedKernel.Interfaces;

public class UserPrivileges
{
  public ICollection<string>? Roles { get; private set; }
  public ICollection<string>? Permissions { get; private set; }

  public static UserPrivileges Create(ICollection<string>? roles, ICollection<string>? permissions)
  {
    return new() { Permissions = permissions, Roles = roles };
  }
}
