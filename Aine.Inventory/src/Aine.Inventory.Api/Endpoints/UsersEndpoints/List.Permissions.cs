using Aine.Inventory.SharedKernel.Security.Interfaces;
using FastEndpoints;
using static Aine.Inventory.Api.Endpoints.AuthEndpoint.ListPermissions;

namespace Aine.Inventory.Api.Endpoints.AuthEndpoint;

public class ListPermissions: EndpointWithoutRequest<IEnumerable<PermissionRecord>>
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

  public override async Task<IEnumerable<PermissionRecord>> ExecuteAsync(CancellationToken cancellationToken)
  {
    var permissions =  await _userService.GetPermissionsAsync();
    return permissions.Select(p => new PermissionRecord(p.Id, p.PermissionTitle, p.Description, p.PermissionType?.ToString()));
  }

  public record PermissionRecord(int Id, string PermissionTitle, string? Description, string? PermissionType);
}