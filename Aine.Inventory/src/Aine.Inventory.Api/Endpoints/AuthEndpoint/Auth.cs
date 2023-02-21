using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aine.Inventory.Api.Endpoints.AuthEndpoint;

public class Auth : Endpoint<AuthRequest, AuthResponse>
{
  private readonly IUserAuthenticator _authenticator;

  public Auth(IUserAuthenticator authenticator)
  {
    _authenticator = authenticator;
  }

  public override void Configure()
  {
    Verbs(Http.POST);
    Routes("/auth");
    AllowAnonymous();
  }

  public override async Task HandleAsync(AuthRequest request, CancellationToken ct)
  {
    var userModel = new UserModel(request.UserName, request.Password, request.CorpName);
    var result = await _authenticator.AuthenticateUserAsync(userModel);
    if (!result.IsSuccess)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }

    var token = GenerateFastEndpointsJWTToken(result.Value);

    await SendAsync(new AuthResponse(token));
  }

  private string GenerateFastEndpointsJWTToken(IUser user)
  {
    var tokenSigningKey = Config["Jwt:Key"] ?? "NO_KEY";

    var jwtToken = JWTBearer.CreateToken(
                signingKey: tokenSigningKey,
                expireAt: DateTime.UtcNow.AddDays(1),
                claims: new[] {
                  ("Name", user.UserName),
                  ("UserName", user.UserName),
                  ("UserId", user.UserId.ToString()),
                  ("CorpName", user.CorpName ?? "Default") 
                },
                roles: user.Roles,
                permissions: user.Permissions);

    return jwtToken;
  }

  private string GenerateJWTToken(AuthRequest request)
  {
    var issuer = Config["Jwt:Issuer"];
    var audience = Config["Jwt:Audience"];
    var key = Encoding.ASCII.GetBytes(Config["Jwt:Key"] ?? "NO_KEY");

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[]
      {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, request.UserName),
                new Claim(JwtRegisteredClaimNames.Email, request.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
             }),
      Expires = DateTime.UtcNow.AddHours(24),
      Issuer = issuer,
      Audience = audience,
      SigningCredentials = new SigningCredentials
        (new SymmetricSecurityKey(key),
        SecurityAlgorithms.HmacSha512Signature)
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    //var jwtToken = tokenHandler.WriteToken(token);
    return tokenHandler.WriteToken(token);
  }
}

public record AuthResponse(string Token);
