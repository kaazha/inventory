using System.Security.Claims;
using Aine.Inventory.SharedKernel.Security;
using Aine.Inventory.SharedKernel.Security.Interfaces;

namespace Aine.Inventory.Api;

public static class Extensions
{
  public static IUser AsUser(this ClaimsPrincipal principal)
  {
    return UserIdentity.Create(
        userId: principal.FindFirstValue("UserId").AsInt(),
        userName: principal.FindFirstValue("UserName"),
        fullName: principal.FindFirstValue("UserFullName")
        );
  }

  public static string UserName(this ClaimsPrincipal principal) => principal.FindFirstValue("UserName");

  public static int AsInt(this string value, int @default = default)
  {
    if (string.IsNullOrEmpty(value)) return @default;
    return int.TryParse(value, out var intValue) ? intValue : @default; 
  }
}

