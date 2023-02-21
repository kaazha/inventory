using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using Ardalis.Specification;

namespace Aine.Inventory.Core.Interfaces;

public interface IProductRepository : IRepositoryBase<Product>
{
  /// <summary> Creates a New Product</summary>
  Task<Product> CreateProductAsync(IProduct product, IUser user, CancellationToken cancellationToken);

  /// <summary> Updates a Product</summary>
  Task<Product> UpdateProductAsync(IProduct product, IUser user, CancellationToken cancellationToken);
}
