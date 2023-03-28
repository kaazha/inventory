using Aine.Inventory.SharedKernel.Interfaces;
using Aine.Inventory.SharedKernel.Security;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using Ardalis.Result;

namespace Aine.Inventory.SharedKernel.Security.Interfaces;

public interface IUserAuthenticator
{
  Task<Result<IUser>> AuthenticateUserAsync(AuthenticationRequest user);
}

public record AuthenticationRequest(string UserName, string Password, string? CorpName = default);
