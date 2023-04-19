namespace Aine.Inventory.Core.TransactionAggregate;

public class TransactionSearchOptions
{
  public int? ProductId { get; set; }
  public string? ProductNumber { get; set; }
  public DateTime? TransactionDateStart { get; set; }
  public DateTime? TransactionDateEnd { get; set; }
  public string? ReferenceNumber { get; set; }
  public ICollection<string>? TransactionTypes { get; set; }
  /// <summary>
  /// The [max] number of rows to return
  /// </summary>
  public int? Take { get; set; }

  public bool IsValid() =>
    ProductId > 0 ||
    !string.IsNullOrEmpty(ProductNumber) ||
    !string.IsNullOrEmpty(this.ReferenceNumber) ||
   TransactionTypes != null ||
    Take > 0 ||
    TransactionDateStart is { } ||
    (TransactionDateEnd is { } && (TransactionDateStart is null || TransactionDateEnd >= TransactionDateStart));
}
