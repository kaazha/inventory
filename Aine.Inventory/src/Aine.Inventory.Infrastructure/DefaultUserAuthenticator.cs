using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.SharedKernel;
using Ardalis.Result;

namespace Aine.Inventory.Infrastructure;

[Inject]
internal class DefaultUserAuthenticator  : IUserAuthenticator
{
  public Task<Result> AuthenticateUserAsync(UserModel user)
  {
    return Task.FromResult(Result.Success());
  }
}
