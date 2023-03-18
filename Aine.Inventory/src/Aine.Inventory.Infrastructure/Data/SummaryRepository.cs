using System;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.Models;
using Aine.Inventory.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Aine.Inventory.Infrastructure.Data;

[Inject]
public class SummaryRepository : ISummaryRepository
{
  private AppDbContext _context;

  public SummaryRepository(AppDbContext dbContext)
  {
    _context = dbContext;
  }

  public async Task<CategoryAndProductSummary> GetCategoryAndProductSummary(CancellationToken cancellationToken = default)
  {
    var totalCategories = await _context.Categories.CountAsync(cancellationToken);
    var totalProducts = await _context.Products.CountAsync(cancellationToken);
    var products = await (
                  from p in _context.Products
                   group p by p.SubCategory!.Category!.Name
                   into g
                   select new ProductCountByCategory(g.Key,  g.Count())
                   ).ToListAsync();

    return new CategoryAndProductSummary
    {
      TotalCategories = totalCategories,
      TotalProducts = totalProducts,
      TotalProductsByCategory = products
    };
  }
}

