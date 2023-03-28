using System.Linq;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Security;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.AuthEndpoint;

public class Create : Endpoint<CreateUserRequest, object>
{
  private readonly IUserService _userService;
  private readonly IEncryptor _encryptor;

  public Create(IUserService userService, IEncryptor encryptor)
  {
    _userService = userService;
    _encryptor = encryptor;
  }

  public override void Configure()
  {
    Post("/users");
    Claims("UserId", "UserName");
    Roles("Admin", "Manager");
    Permissions("Create User", "Create Users");
    //Policy(x => x.RequireAssertion(...));
  }

  public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
  {
    var user = SharedKernel.Security.User.Create(
      0,
      req.UserId,
      req.UserFullName,
      req.Password,
      null,
      User.UserName(),
      req.Roles,
      req.Permissions
      );

    var result = await _userService.CreateUserAsync(user, _encryptor, ct);
    if (!result.IsSuccess)
      ThrowError(result.Errors.Join(Environment.NewLine));
    var userCreated = result.Value;
    Response = new { UserId = userCreated.Id };
  }
}
