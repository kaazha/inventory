using Aine.Inventory.Core.CategoryAggregate;
using Ardalis.Specification;

namespace Aine.Inventory.Core.Interfaces;

// from Ardalis.Specification
public interface ICategoryRepository : IRepositoryBase<ProductCategory>
{
  Task<int> DeleteByIdAsync(int id, CancellationToken cancellationToken);
}
