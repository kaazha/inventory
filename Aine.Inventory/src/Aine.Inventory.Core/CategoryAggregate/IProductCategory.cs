namespace Aine.Inventory.Core.CategoryAggregate;

public interface ICategory
{
  int Id { get; }
  string? Name { get; }
  string? Description { get; }
  IEnumerable<ISubCategory>? SubCategories { get; }
}