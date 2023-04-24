using System;
using System.Linq;
using Aine.Inventory.Core;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.Models;
using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.TransactionAggregate;
using Aine.Inventory.SharedKernel;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aine.Inventory.Infrastructure.Data;

[Inject]
public class SummaryRepository : ISummaryRepository
{
  private AppDbContext _context;
  private readonly ISettings _settings;

  public SummaryRepository(AppDbContext dbContext, ISettings settings)
  {
    _context = dbContext;
    _settings = settings;
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

  public async Task<IReadOnlyCollection<ProductTransactionSummary>> GetSummary(TransactionSearchOptions options,
    CancellationToken cancellationToken = default)
  {
    var predicate = options.Predicate();

    var query = from t in _context.Transactions.Where(predicate)
                group t by new { t.Product!.Name, t.Product!.ProductNumber } into g
                orderby g.Sum(t => t.Quantity) descending
                select new ProductTransactionSummary
                {
                  ProductNumber = g.Key.ProductNumber,
                  ProductName = g.Key.Name,
                  Quantity = g.Sum(t => t.Quantity),
                  Amount = g.Sum(t => t.TotalCost ?? 0)
                };

    var products = await query.Take(options.Take.GetValueOrDefault()).ToListAsync();
    return products.Select(t =>
    {
      t.Amount = Math.Round(t.Amount, _settings.DecimalPlaces);
      return t;
    })
      .ToList();
  }
}

