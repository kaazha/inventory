using System.Linq.Expressions;
using Ardalis.Specification;
using Aine.Inventory.SharedKernel;
using static System.Math;

namespace Aine.Inventory.Core.TransactionAggregate;

public class TransactionSearchSpecification : Specification<ProductTransaction, TransactionDto>
{
  public TransactionSearchSpecification(TransactionSearchOptions options)
  {
    Query
        .Select(t => new TransactionDto
        {
          TransactionId = t.Id,
          TransactionType = t.TransactionType,
          TransactionDate = t.TransactionDate,
          ProductId = t.ProductId,
          ProductNumber = t.Product!.ProductNumber,
          ReferenceNumber = t.ReferenceNumber,
          ProductName = t.Product.Name,
          TotalCost = t.TotalCost != null ? Round(t.TotalCost.Value, 2) : null,
          Quantity = t.Quantity,
          CreatedBy = t.CreatedBy,
          DateCreated = t.DateCreated,
          ModifiedBy = t.ModifiedBy,
          ModifiedDate = t.ModifiedDate,
          Notes = t.Notes
        })
        .Where(options.Predicate())
        .OrderByDescending(p => p.TransactionDate);

    if (options.Take > 0) Query.Take(options.Take.Value);
  }
}
