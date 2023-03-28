using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.IdentityModel.Tokens;

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
    Summary(s =>
    { 
      s.Summary = "Authenticates a User";
      s.Response(200, "JWT Token");
    });
  }

  public override async Task HandleAsync(AuthRequest request, CancellationToken ct)
  {
    var userModel = new AuthenticationRequest(request.UserName, request.Password, request.CorpName);
    var result = await _authenticator.AuthenticateUserAsync(userModel);
    if (!result.IsSuccess)
    {
      await SendStringAsync(result.Errors?.FirstOrDefault() ?? "Invalid credentials", StatusCodes.Status400BadRequest);
      return;
    }

    var token = GenerateFastEndpointsJWTToken(result.Value);

    await SendAsync(new AuthResponse(token), cancellation: ct);
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
                  //("CorpName", user.CorpName ?? "Default") 
                },
                roles: user?.Roles?.Select(r => r.RoleName),
                permissions: user?.Permissions?.Select(r => r.PermissionName ?? string.Empty)
              );

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
