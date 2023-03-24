
namespace Aine.Inventory.SharedKernel.Interfaces;

public interface IUser
{
  int UserId { get; }
  string UserName { get; }
  string? CorpName { get; }
  string FullName { get; }
  UserPrivileges Privileges { get; }
}
