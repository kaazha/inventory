using Aine.Inventory.SharedKernel.Security;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using Aine.Inventory.SharedKernel.Security.Specifications;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.AuthEndpoint;

public class List : EndpointWithoutRequest<IEnumerable<IUser>>
{
  private readonly IUserService _userService;

  public List(IUserService userService)
  {
    _userService = userService;
  }

  public override void Configure()
  {
    Get("/users");
    Claims("UserId", "UserName");
    Roles("Admin", "Manager");
    Permissions("View Users");
  }

  public override async Task<IEnumerable<IUser>> ExecuteAsync(CancellationToken cancellationToken)
  {
    var specification = new UserSpecification();
    var users = await _userService.GetUsersAsync(specification, cancellationToken);
    return users;
  }
}