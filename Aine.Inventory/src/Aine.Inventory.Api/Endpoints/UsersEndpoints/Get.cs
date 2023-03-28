using System.Threading;
using Aine.Inventory.SharedKernel.Interfaces;
using Aine.Inventory.SharedKernel.Security;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using Aine.Inventory.SharedKernel.Security.Specifications;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.AuthEndpoint;

public class GetUser : EndpointWithoutRequest<IUser>
{
  private readonly IReadRepository<User> _repository;

  public GetUser(IReadRepository<User> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/users/{userId}");
    Claims("UserId", "UserName");
    //Roles("Admin", "Manager");
    Permissions("View Users");
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var userId = Route<string>("userId");
    var specification = new UserByIdSpecification(int.TryParse(userId, out var id) ? id : null, userId);
    var user = await _repository.FirstOrDefaultAsync(specification, cancellationToken);
    if(user == null)
    {
      await SendNotFoundAsync(cancellationToken);
    }
    await SendOkAsync(user!, cancellationToken);
  }
}