using Aine.Inventory.SharedKernel.Interfaces;
using Ardalis.Result;

namespace Aine.Inventory.Core.Interfaces;

public interface IUserAuthenticator
{
  Task<Result<IUser>> AuthenticateUserAsync(UserModel user);
}

public record UserModel(string UserName, string Password, string? CorpName = default);
