namespace Aine.Inventory.Core.TransactionAggregate;

/// <summary>
/// Represents search options used to query best performing
/// (e.g. best selling) products during a specified period (e.g. current week, this month etc)
/// </summary>
public class BestPerformingTransactionOptions : RecentTransactionOptions
{
  public const int DEFAULT_COUNT = 10;

  public override TransactionSearchOptions ToTransactionSearchOptions()
  {
    if (this.Take.GetValueOrDefault() <= 0) this.Take = DEFAULT_COUNT;
    if (this.TransactionTypes == null || !this.TransactionTypes.Any())
      TransactionTypes = new[] { TransactionType.Sales.Code };
    return base.ToTransactionSearchOptions();
  }
}