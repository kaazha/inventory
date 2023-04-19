using System;
using System.Linq.Expressions;
using Aine.Inventory.SharedKernel;
using Ardalis.Specification;

namespace Aine.Inventory.Core.TransactionAggregate;

public class TransactionSpecification : Specification<ProductTransaction>
{

  public TransactionSpecification(int? productId = default, string? transactionType = default)
  {
    Expression<Func<ProductTransaction, bool>>? predicate = null;
    if (productId > 0) predicate = predicate.AndAlso(p => p.ProductId == productId!);
    if (!string.IsNullOrEmpty(transactionType)) predicate = predicate.AndAlso(p => p.TransactionType == transactionType);

    if (predicate != null)
    {
      Query.Where(predicate);
    }

    Query
        .OrderBy(p => p.ProductId)
        .ThenByDescending(p => p.TransactionDate);
  }
}

