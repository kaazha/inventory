namespace Aine.Inventory.Core.CategoryAggregate;

public interface ISubCategory
{
  int Id { get; }
  int CategoryId { get; }
  string? Name { get; }
  string? Description { get; }
}