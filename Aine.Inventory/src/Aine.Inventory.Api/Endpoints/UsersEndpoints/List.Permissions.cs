using Aine.Inventory.SharedKernel.Security;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.AuthEndpoint;

public class ListPermissions: EndpointWithoutRequest<IEnumerable<Permission>>
{
  private readonly IUserService _userService;

  public ListPermissions(IUserService userService)
  {
    _userService = userService;
  }

  public override void Configure()
  {
    Get("/users/permissions");
    //AllowAnonymous();
  }

  public override async Task<IEnumerable<Permission>> ExecuteAsync(CancellationToken cancellationToken)
  {
    return await _userService.GetPermissionsAsync();
  }
}