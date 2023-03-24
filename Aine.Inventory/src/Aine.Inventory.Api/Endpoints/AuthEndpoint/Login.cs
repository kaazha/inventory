using Aine.Inventory.Core.Interfaces;
using FastEndpoints;
using FastEndpoints.Security;
using MediatR;

namespace Aine.Inventory.Api.Endpoints.AuthEndpoint;

public class LoginEndpoint : Endpoint<AuthRequest, TokenResponse>
{
  private readonly IUserAuthenticator _authenticator;

  public LoginEndpoint(IUserAuthenticator authenticator)
  {
    _authenticator = authenticator;
  }

  public override void Configure()
  {
    Get("/api/login");
    AllowAnonymous();
    Summary(s =>
    {
      s.Summary = "Authenticates a User";
      s.Response(200, "JWT Token");
    });
  }

  public override async Task HandleAsync(AuthRequest request, CancellationToken cancellationToken)
  {
    var userModel = new UserModel(request.UserName, request.Password, request.CorpName);
    var result = await _authenticator.AuthenticateUserAsync(userModel);
    if (!result.IsSuccess)
      ThrowError("Invalid user credentials!");

    Response = await CreateTokenWith<AuthTokenService>(result.Value.UserName, u =>
    {
      u.Roles.AddRange(new[] { "Admin", "Manager" });
      u.Permissions.Add("Update_Something");
      u.Claims.Add(new("UserId", "user-id-001"));
    });
  }
}