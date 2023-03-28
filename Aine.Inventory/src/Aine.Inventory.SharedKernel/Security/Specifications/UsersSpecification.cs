using System;
using System.Linq.Expressions;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using Ardalis.Specification;

namespace Aine.Inventory.SharedKernel.Security.Specifications;

public class UserSpecification : Specification<User, UserIdentity>
{
  public UserSpecification()
  {
    SetupQuery();
  }

  protected UserSpecification(Expression<Func<User, bool>> criteria)
  {
    SetupQuery(criteria);
  }

  protected virtual void SetupQuery(Expression<Func<User, bool>>? criteria = default)
  {
    if (criteria != null)
    {
      Query.Where(criteria);
    }

    Query
      .Select(u => new UserIdentity
      {
        UserId = u.Id,
        UserName = u.UserName,
        FullName = u.FullName,
        IsActive = u.IsActive,
        CreatedBy = u.CreatedBy,
        DateCreated = u.DateCreated,
        LastLogIn = u.LastLogIn,
        LastUpdated = u.LastUpdated,
        LastUpdatedBy = u.LastUpdatedBy,
        Permissions = u.Permissions!.Select(p =>
          new UserPermissionInfo(p.PermissionId, p.Permission.PermissionTitle, p.Permissions)),
        Roles = u.Roles!.Select(r => new UserRoleInfo(r.RoleId, r.Role.RoleName, r.Role.IsAdminRole))
      })
    .Include(u => u.Permissions!)
      .ThenInclude(p => p.Permission)
    .Include(u => u.Roles!)
       .ThenInclude(p => p.Role)
    .AsSplitQuery();
  }
}