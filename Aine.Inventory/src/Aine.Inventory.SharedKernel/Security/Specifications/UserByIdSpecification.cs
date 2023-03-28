using System.Linq.Expressions;

namespace Aine.Inventory.SharedKernel.Security.Specifications;

public class UserByIdSpecification : UserSpecification
{
  public UserByIdSpecification(int? userId, string? userName) : base(GetPredicate(userId, userName)) { }

  private static Expression<Func<User, bool>> GetPredicate(int? userId, string? userName)
  {
    if (userId.HasValue && userId != 0)
      return u => u.Id == userId;

    if (!string.IsNullOrEmpty(userName))
      return u => u.UserName == userName;

    return u => true;
  }
}
