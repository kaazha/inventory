using System;
using System.Security.Cryptography;
using Aine.Inventory.Core.Interfaces;
using FastEndpoints;
using FastEndpoints.Security;

namespace Aine.Inventory.Api.Endpoints.AuthEndpoint;

public class AuthTokenService : RefreshTokenService<TokenRequest, TokenResponse>
{
  private readonly ITokenRepository _repository;

  public AuthTokenService(IConfiguration config, ITokenRepository repository)
  {
    _repository = repository;

    Setup(o =>
    {
      o.TokenSigningKey = config["Jwt:Key"];
      o.AccessTokenValidity = TimeSpan.FromHours(24);
      o.RefreshTokenValidity = TimeSpan.FromHours(24);

      o.Endpoint("/refresh-token", ep =>
      {
        ep.Summary(s => s.Summary = "this is the refresh token endpoint");
      });
    });
  }

  public override async Task PersistTokenAsync(TokenResponse response)
  {
    await _repository.StoreToken(response.UserId, response.RefreshExpiry, response.RefreshToken);

    // this method will be called whenever a new access/refresh token pair is being generated.
    // store the tokens and expiry dates however you wish for the purpose of verifying
    // future refresh requests.        
  }

  public override async Task RefreshRequestValidationAsync(TokenRequest req)
  {
    if (!await _repository.TokenIsValid(req.UserId, req.RefreshToken))
      AddError(r => r.RefreshToken, "Refresh token is invalid!");

    // validate the incoming refresh request by checking the token and expiry against the
    // previously stored data. if the token is not valid and a new token pair should
    // not be created, simply add validation errors using the AddError() method.
    // the failures you add will be sent to the requesting client. if no failures are added,
    // validation passes and a new token pair will be created and sent to the client.        
  }

  public override Task SetRenewalPrivilegesAsync(TokenRequest request, UserPrivileges privileges)
  {
    //privileges.Roles.Add("Manager");
    //privileges.Claims.Add(new("ManagerID", request.UserId));
    //privileges.Permissions.Add("Manage_Department");

    // specify the user privileges to be embedded in the jwt when a refresh request is
    // received and validation has passed. this only applies to renewal/refresh requests
    // received to the refresh endpoint and not the initial jwt creation.

    return Task.CompletedTask;
  }
}
