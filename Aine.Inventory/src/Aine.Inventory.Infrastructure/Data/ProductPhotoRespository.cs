using System;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductPhotoAggregate;
using Aine.Inventory.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Aine.Inventory.Infrastructure.Data;

[Inject]
public class ProductPhotoRepository : EfRepository<ProductPhoto>, IProductPhotoRepository
{
  private AppDbContext _context;

  public ProductPhotoRepository(AppDbContext dbContext) : base(dbContext)
  {
    _context = dbContext;
  }

  public async Task<int> UpdateProductPhotoAsync(int productId, string productPhotoFileName)
  {
    var dbSet = _context.Set<ProductPhoto>();
    if (!await dbSet.AnyAsync(p => p.ProductId == productId))
    {
      var productPhoto = new ProductPhoto(productId, productPhotoFileName, productPhotoFileName);
      dbSet.Add(productPhoto);
      return await _context.SaveChangesAsync();
    }

    return await dbSet
          .Where(p => p.ProductId == productId)
          .ExecuteUpdateAsync(
            p => p
                .SetProperty(b => b.ThumbNailPhotoFileName, productPhotoFileName)
                .SetProperty(b => b.LargePhotoFileName, productPhotoFileName)
          );
  }
}

