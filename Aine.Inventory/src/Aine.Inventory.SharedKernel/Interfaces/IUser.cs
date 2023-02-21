
namespace Aine.Inventory.SharedKernel.Interfaces;

public interface IUser
{
  public int UserId { get; }
  public string UserName { get; }
  public string? CorpName { get; }
  public ICollection<string>? Roles { get; }
  public ICollection<string>? Permissions { get; }
}