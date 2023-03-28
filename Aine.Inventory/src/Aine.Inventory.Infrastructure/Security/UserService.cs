using System.Threading;
using Aine.Inventory.Infrastructure.Data;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Interfaces;
using Aine.Inventory.SharedKernel.Security;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using Ardalis.Result;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using static System.String;

namespace Aine.Inventory.Infrastructure.Security;

[Inject]
public class UserService : IUserService
{
  private readonly IRepository<User> _repository;
  private readonly AppDbContext _context;

  public UserService(IRepository<User> repository, AppDbContext context)
  {
    _repository = repository;
    _context = context;
  }

  public async Task<Result<User>> CreateUserAsync(User user, IEncryptor encryptor, CancellationToken cancellationToken)
  {
    var errors = new List<string>();

    if (string.IsNullOrEmpty(user.UserName))
      errors.Add("User ID is required.");
    if (string.IsNullOrEmpty(user.Password))
      errors.Add("User Password is required.");
    if (string.IsNullOrEmpty(user.FullName))
      errors.Add("User Full Name is required.");
    if(await Exists(user.UserName))
      errors.Add($"User ID '{user.UserName}' is takem.");

    if (errors.Any())
        return Result.Error(errors.ToArray());

    // populate Permission and Role IDs
    await SetObjectIds(user.Roles, GetRolesByNameAsync);
    await SetObjectIds(user.Permissions, GetPermissionsByNameAsync);

    user.EncryptPassword(encryptor);
    return await _repository.AddAsync(user, cancellationToken);
  }

  public async Task<User?> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
  {
    return await _repository.GetByIdAsync(userId, cancellationToken);
  }

  public async Task<ICollection<TResult>> GetUsersAsync<TResult>(
    ISpecification<User, TResult> specification,
    CancellationToken cancellationToken
    )
  {
    return await _repository.ListAsync(specification, cancellationToken);
  }

  public async Task<TResult?> FindUserAsync<TResult>(ISpecification<User, TResult> specification)
  {
    return await _repository.FirstOrDefaultAsync(specification);
  }

  public async Task<ICollection<Role>> GetRolesAsync() => await _context.Set<Role>().ToListAsync();

  private async Task<IEnumerable<ISecurityObject>> GetRolesByNameAsync(IEnumerable<string>? roleNames)
  {
    if (roleNames == null || !roleNames.Any()) return Array.Empty<Role>();
    return await _context.Set<Role>()
              .Where(r => roleNames.Contains(r.RoleName))
              .ToListAsync();
  }

  private async Task<IEnumerable<ISecurityObject>> GetPermissionsByNameAsync(IEnumerable<string>? permissionNames)
  {
    if (permissionNames == null || !permissionNames.Any()) return Array.Empty<Permission>();
    return await _context.Set<Permission>()
              .Where(r => permissionNames.Contains(r.PermissionTitle))
              .ToListAsync();
  }

  public async Task<ICollection<Permission>> GetPermissionsAsync() => await _context.Set<Permission>().ToListAsync();

  private static async Task SetObjectIds(IEnumerable<ISecurityObject>? securityObjects, Func<IEnumerable<string>, Task<IEnumerable<ISecurityObject>>> getObjectsByName)
  {
    if (securityObjects == null || !securityObjects.Any(r => r.Id == 0 && !IsNullOrEmpty(r.Name))) return;

    var nameOnlyObjs = securityObjects.Where(r => r.Id == 0 && !IsNullOrEmpty(r.Name));
    var names = nameOnlyObjs.Select(r => r.Name ?? Empty);
    var objs = await getObjectsByName(names);
    var objsByName = objs!.ToDictionary(o => o.Name ?? Empty, StringComparer.OrdinalIgnoreCase);
    nameOnlyObjs.ForEachItem(r => r.Id = objsByName.TryGetValue(r.Name ?? Empty, out var role) ? role.Id : r.Id);
  }

  private async Task<bool> Exists(string userName)
  {
    return await _repository.AnyAsync(new UserSpecification(userName));
  }

  private class UserSpecification : Specification<User>
  {
    public UserSpecification(string userName)
    {
      Query.Where(u => u.UserName == userName);
    }
  }
}

