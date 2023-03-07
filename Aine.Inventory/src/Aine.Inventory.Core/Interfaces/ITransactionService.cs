using System;
using Aine.Inventory.Core.TransactionAggregate;
using Ardalis.Result;

namespace Aine.Inventory.Core.Interfaces;

public interface ITransactionService
{
  Task<Result<IEnumerable<ProductTransaction>>> CreateTransaction(CreateTransactionRequest transaction, CancellationToken cancellationToken);
}

public class CreateTransactionRequest
{
  public DateTime TransactionDate { get; set; }
  public string TransactionType { get; set; } = default!;
  public string? ReferenceNumber { get; set; }
  public string? Notes { get; set; }
  public string? UserName { get; set; }
  public IList<TransactionItem> Items { get; set; } = new List<TransactionItem>();
}

public class TransactionItem
{
  public int ProductId { get; set; }
  public int Quantity { get; set; }
  public double? TotalCost { get; set; }
}
