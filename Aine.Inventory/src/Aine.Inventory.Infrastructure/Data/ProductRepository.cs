using System;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aine.Inventory.Infrastructure.Data;

[Inject]
public class ProductRepository : EfRepository<Product>, IProductRepository
{
  private AppDbContext _context;

  public ProductRepository(AppDbContext dbContext) : base(dbContext)
  {
    _context = dbContext;
  }

  /// <summary> Creates a New Product</summary>
  public async Task<Product> CreateProductAsync(IProduct product, IUser user, CancellationToken cancellationToken)
  {
    var newProduct = Product.Create(product);
    newProduct.CreatedBy = user.UserName;
    var createdItem = await AddAsync(newProduct, cancellationToken);
    return createdItem;
  }

  /// <summary> Updates a Product</summary>
  public async Task<Product> UpdateProductAsync(IProduct updatedProduct, IUser user, CancellationToken cancellationToken)
  {
    var product = Product.Create(updatedProduct);
    product.ModifiedBy = user.UserName;
    var transaction = await _context.Database.BeginTransactionAsync();
    await _context.ProductInventories.Where(p => p.ProductId == product.Id).ExecuteDeleteAsync();
    await UpdateAsync(product, cancellationToken);
    await transaction.CommitAsync();
    return product;
  }

  public async Task<ICollection<ProductInfo>> GetProducts(CancellationToken cancellationToken)
  {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    return await _context
        .Set<Product>()
        .OrderBy(p => p.SubCategory.CategoryId)
        .ThenBy(p => p.ProductNumber)
        .Select(p => new ProductInfo(p.Id, p.ProductNumber, p.Name, p.SubCategory.Category.Name))
        .ToListAsync(cancellationToken);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }

  public async Task<int> UpdateProductListPrice(int productId, double listPrice, object? user)
  {
    var updatedBy = user is IUser usr ? usr.UserName : (user?.ToString() ?? "unknown user");
    return await _context.Set<Product>()
        .Where(p => p.Id == productId)
        .ExecuteUpdateAsync(p =>
          p.SetProperty(x => x.ListPrice, listPrice)
           .SetProperty(x => x.ModifiedBy, updatedBy)
           .SetProperty(x => x.ModifiedDate, DateTime.Now)
          );
  }
}

