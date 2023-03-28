namespace Aine.Inventory.SharedKernel.Security.Interfaces;

public interface ISecurityObject
{
  int Id { get; set; }
  string? Name { get; }
}
