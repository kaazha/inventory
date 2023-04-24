using Ardalis.Specification;

namespace Aine.Inventory.Core.TransactionAggregate;

public class ProductTransactionSummary
{
  public string ProductNumber { get; set; } = default!;
  public string ProductName { get; set; } = default!;
  public int Quantity { get; set; } = default!;
  public double Amount { get; set; } = default!;
}
