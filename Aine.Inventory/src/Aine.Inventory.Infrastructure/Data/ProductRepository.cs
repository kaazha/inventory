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
}

