using Aine.Inventory.SharedKernel.Security;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.AuthEndpoint;

public class ListRoles : EndpointWithoutRequest<IEnumerable<Role>>
{
  private readonly IUserService _userService;

  public ListRoles(IUserService userService)
  {
    _userService = userService;
  }

  public override void Configure()
  {
    Get("/users/roles");
    //AllowAnonymous();
  }

  public override async Task<IEnumerable<Role>> ExecuteAsync(CancellationToken cancellationToken)
  {
    return await _userService.GetRolesAsync();
  }
}