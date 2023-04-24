using System;
using Aine.Inventory.Core.TransactionAggregate;
using System.Linq.Expressions;

namespace Aine.Inventory.Core;

public static class CoreExtensions
{
  public static Expression<Func<ProductTransaction, bool>> Predicate(this TransactionSearchOptions options)
  {
    if (!options.IsValid()) throw new ArgumentException("At least one transaction search option must be specified!");
    var transactionTypes = options.TransactionTypes?
                .Select(type => TransactionType.FromName(type)?.Code)?
                .Where(c => c != null)?.ToArray()
                ?? Array.Empty<string>();

    Expression<Func<ProductTransaction, bool>>? predicate = null; // p => true;
    predicate = predicate.AndAlso(p => p.ProductId == options.ProductId, options.ProductId > 0)
                         .AndAlso(p => p.Product!.ProductNumber.Contains(options.ProductNumber!), !string.IsNullOrEmpty(options.ProductNumber))
                         .AndAlso(p => transactionTypes.Contains(p.TransactionType), transactionTypes != null && transactionTypes.Any())
                         .AndAlso(p => p.ReferenceNumber!.Contains(options.ReferenceNumber!), !string.IsNullOrEmpty(options.ReferenceNumber))
                         .AndAlso(p => p.TransactionDate >= options.TransactionDateStart, options.TransactionDateStart != null) // inludes start date
                         .AndAlso(p => p.TransactionDate < options.TransactionDateEnd, options.TransactionDateEnd != null);  // excludes end date

    return predicate ?? (p => true);
  }
}

