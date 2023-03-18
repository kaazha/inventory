using System;
using System.Security.Claims;
using Aine.Inventory.SharedKernel.Interfaces;
using Aine.Inventory.SharedKernel.Security;

namespace Aine.Inventory.Api;

public static class Extensions
{
  public static IUser AsUser(this ClaimsPrincipal principal)
  {
    return User.Create(
        userId: principal.FindFirstValue("UserId").AsInt(),
        userName: principal.FindFirstValue("UserName")
        );
  }

  public static string UserName(this ClaimsPrincipal principal) => principal.FindFirstValue("UserName");

  public static int AsInt(this string value, int @default = default)
  {
    if (string.IsNullOrEmpty(value)) return @default;
    return int.TryParse(value, out var intValue) ? intValue : @default; 
  }
}

