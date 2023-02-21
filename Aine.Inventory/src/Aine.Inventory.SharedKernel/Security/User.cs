
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.SharedKernel.Security;

public class User : IUser
{
  public int UserId { get; private set; }
  public string UserName { get; private set; } = default!;
  public string? CorpName { get; private set; }
  public ICollection<string>? Roles { get; private set; }
  public ICollection<string>? Permissions { get; private set; }

  public static User Create (
    int userId, 
    string userName, 
    string? corpName = default, 
    ICollection<string>? roles = default, 
    ICollection<string>? permissions = default)
  {
    return new User
    {
      UserId = userId,
      UserName = userName,
      CorpName = corpName,
      Roles = roles,
      Permissions = permissions
    };
  }
}
