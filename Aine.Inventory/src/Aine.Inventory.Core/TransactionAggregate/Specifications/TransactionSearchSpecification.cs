using System.Linq.Expressions;
using Ardalis.Specification;
using Aine.Inventory.SharedKernel;
using static System.Math;

namespace Aine.Inventory.Core.TransactionAggregate;

public class TransactionSearchSpecification : Specification<ProductTransaction, TransactionDto>
{
  public TransactionSearchSpecification(TransactionSearchOptions options)
  {
    ArgumentNullException.ThrowIfNull(options, "TransactionSearchOptions");
    if (!options.IsValid()) throw new ArgumentException("At least one transaction search option must be specified!");
    var transactionTypes = options.TransactionTypes?.Select(type => type switch
    {
      "Sales" => "S",
      "Inflow" => "P",
      "Outflow" => "W",
      _ => type
    })?.ToArray() ?? Array.Empty<string>();

    Expression<Func<ProductTransaction, bool>> predicate = p => true;
    predicate = predicate.AndAlso(p => p.ProductId == options.ProductId, options.ProductId > 0)
                         .AndAlso(p => p.Product!.ProductNumber.Contains(options.ProductNumber!), !string.IsNullOrEmpty(options.ProductNumber))
                         .AndAlso(p => transactionTypes.Contains(p.TransactionType), transactionTypes != null && transactionTypes.Any())
                         .AndAlso(p => p.ReferenceNumber!.Contains(options.ReferenceNumber!), !string.IsNullOrEmpty(options.ReferenceNumber))
                         .AndAlso(p => p.TransactionDate >= options.TransactionDateStart, options.TransactionDateStart != null) // inludes start date
                         .AndAlso(p => p.TransactionDate < options.TransactionDateEnd, options.TransactionDateEnd != null);  // excludes end date
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
        .Where(predicate)
        .OrderByDescending(p => p.TransactionDate);

    if (options.Take > 0) Query.Take(options.Take.Value);
  }
}
