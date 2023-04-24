using System;
using Aine.Inventory.Core.Models;
using Aine.Inventory.Core.TransactionAggregate;
using Ardalis.Specification;

namespace Aine.Inventory.Core.Interfaces;

public interface ISummaryRepository
{
  Task<CategoryAndProductSummary> GetCategoryAndProductSummary(CancellationToken cancellationToken = default);
  Task<IReadOnlyCollection<ProductTransactionSummary>> GetSummary(TransactionSearchOptions options, CancellationToken cancellationToken = default);
}