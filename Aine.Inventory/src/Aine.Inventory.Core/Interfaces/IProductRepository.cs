using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.SharedKernel.Security.Interfaces;
using Ardalis.Specification;

namespace Aine.Inventory.Core.Interfaces;

public interface IProductRepository : IRepositoryBase<Product>
{
  /// <summary> Creates a New Product</summary>
  Task<Product> CreateProductAsync(IProduct product, IUser user, CancellationToken cancellationToken);

  /// <summary> Updates a Product</summary>
  Task<Product> UpdateProductAsync(IProduct product, IUser user, CancellationToken cancellationToken);

  Task<ICollection<ProductInfo>> GetProducts(CancellationToken cancellationToken);

  Task<int> UpdateProductListPrice(int productId, double listPrice, object? user);
}
