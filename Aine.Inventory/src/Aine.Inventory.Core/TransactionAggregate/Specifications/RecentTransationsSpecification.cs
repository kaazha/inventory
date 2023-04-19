namespace Aine.Inventory.Core.TransactionAggregate;

public class RecentTransationsSpecification : TransactionSearchSpecification
{
  public RecentTransationsSpecification(RecentTransactionOptions options) : base(options.ToTransactionSearchOptions())
  {
  }
}
