using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.Core.ProductAggregate;
using Ardalis.Specification;

namespace Aine.Inventory.Core.Interfaces;

public interface IProductRepository : IRepositoryBase<Product>
{
  /// <summary> Creates a New Product</summary>
  Task<Product> CreateProductAsync(IProduct product, CancellationToken cancellationToken);

  /// <summary> Updates a Product</summary>
  Task<Product> UpdateProductAsync(IProduct product, CancellationToken cancellationToken);
}
