namespace Aine.Inventory.Api.Endpoints.AuthEndpoint;

public class AuthRequest
{
  public string? CorpName { get; set; }
  public string UserName { get; set; } = default!;
  public string Password { get; set; } = default!;
}
