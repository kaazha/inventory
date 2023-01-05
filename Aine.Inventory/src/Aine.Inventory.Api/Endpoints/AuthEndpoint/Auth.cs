using Aine.Inventory.Core.Interfaces;
using FastEndpoints;
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
    var result = await _authenticator.AuthenticateUserAsync(new UserModel(request.UserName, request.Password, request.CorpName));
    if (!result.IsSuccess)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }

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
    var jwtToken = tokenHandler.WriteToken(token);
    var stringToken = tokenHandler.WriteToken(token);
    
    await SendAsync(new AuthResponse(stringToken));
  }
}

public record AuthResponse(string Token);
