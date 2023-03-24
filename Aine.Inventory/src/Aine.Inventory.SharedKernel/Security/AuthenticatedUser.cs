using System;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.SharedKernel.Security;

public class AuthenticatedUser : IUser
{
  public AuthenticatedUser()
  {
  }

  public int UserId { get; private set; }
  public string UserName { get; private set; } = default!;
  public string FullName { get; private set; } = default!;
  public string? CorpName { get; private set; }
  public UserPrivileges Privileges { get; private set; } = new();

  public static AuthenticatedUser Create(
    int userId,
    string userName,
    string? corpName = default,
    string? fullName = default,
    ICollection<string>? roles = default,
    ICollection<string>? permissions = default
    )
  {
    return new AuthenticatedUser
    {
      UserId = userId,
      UserName = userName,
      CorpName = corpName,
      FullName = fullName ?? userName,
      Privileges = UserPrivileges.Create(roles, permissions)
    };
  }
}

