using Ardalis.Result;
using Ardalis.Specification;

namespace Aine.Inventory.SharedKernel.Security.Interfaces;

public interface IUserService
{
  Task<Result<User>> CreateUserAsync(User user, IEncryptor encryptor, CancellationToken cancellationToken);
  Task<User?> GetUserByIdAsync(int userId, CancellationToken cancellationToken);
  Task<ICollection<TResult>> GetUsersAsync<TResult>(ISpecification<User, TResult> specification, CancellationToken cancellationToken);
  Task<TResult?> FindUserAsync<TResult>(ISpecification<User, TResult> specification);
  Task<ICollection<Role>> GetRolesAsync();
  Task<ICollection<Permission>> GetPermissionsAsync();
}