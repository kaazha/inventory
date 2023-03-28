using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using FastEndpoints;
using FastEndpoints.Security;
using MediatR;
using NuGet.Packaging;

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
    Routes("/login");
    Verbs(Http.POST);
    AllowAnonymous();
    Summary(s =>
    {
      s.Summary = "Authenticates a User. Returns Auth Token.";
      s.Response(200, "JWT Token");
    });
  }

  public override async Task HandleAsync(AuthRequest request, CancellationToken cancellationToken)
  {
    var userModel = new AuthenticationRequest(request.UserName, request.Password, request.CorpName);
    var result = await _authenticator.AuthenticateUserAsync(userModel);
    if (!result.IsSuccess)
      ThrowError("Invalid user credentials!");
    var user = result.Value;

    Response = await CreateTokenWith<AuthTokenService>(result.Value.UserName, u =>
    {
      if(user.Roles != null) u.Roles.AddRange(user.Roles.Select(r => r.RoleName));
      if (user.Permissions != null) u.Permissions.AddRange(user.Permissions.Select(r => r.PermissionName!));
      u.Claims.Add(new("UserId", user.UserId.ToString()));
      u.Claims.Add(new("UserName", user.UserName));
      u.Claims.Add(new("Name", user.UserName));
    });
  }
}