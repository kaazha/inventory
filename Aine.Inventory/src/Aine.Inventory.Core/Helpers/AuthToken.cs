using System;
namespace Aine.Inventory.Core.Helpers;

public class AuthToken
{
  public string UserId { get; set; } = default!;
  public DateTime ExpiryDate { get; set; }
  public string Token { get; set; } = default!;
}