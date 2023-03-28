using Aine.Inventory.SharedKernel.Security;

namespace Aine.Inventory.SharedKernel.Security.Specifications;

public class ActiveUserByUserNameAndPasswordSpecification : UserSpecification
{
  public ActiveUserByUserNameAndPasswordSpecification(string userName, string password, string? altPassword = null)
      : base(u => u.IsActive && u.UserName == userName && (u.Password == password || u.Password == altPassword)) { }
}
