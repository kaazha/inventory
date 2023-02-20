using System.IO;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Guard;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.ProductPriceAggregate;

public class ProductPrice : EntityBase<int>, IAggregateRoot, IProductPrice
{
  private ProductPrice() { }

  public int ProductId { get; internal set; }
  public DateTime EffectiveDate { get; private set; }
  public DateTime? EndDate { get; private set; }
  public DateTime? DateChanged { get; private set; }
  public string? ChangedBy { get; private set; }
  public double ListPrice { get; private set; }
  public double? PriceChange { get; private set; }
  public string? Notes { get; private set; }

  public void SetEndDate(DateTime? endDate)
  {
    if (endDate.HasValue && endDate < EffectiveDate)
      throw new ArgumentException($"Price effective date must be before it's end date! (Effective={EffectiveDate}, end={endDate})");
    EndDate = endDate;
  }

  public static ProductPrice Create(
    int id,
    int productId,
    DateTime effectiveDate,
    DateTime? endDate,
    double listPrice,
    double? priceChange,
    string? changedBy,
    DateTime dateChanged,
    string? notes)
  {
    GuardModel.Against.Negative(productId, "Invalid product");
    GuardModel.Against.Negative(listPrice, $"Invalid list price ({listPrice})");
    if (endDate.HasValue && endDate < effectiveDate)
      throw new ArgumentException($"Price effective date must be before it's end date! (Effective={effectiveDate}, end={endDate})");

    return new ProductPrice
    {
      Id = id,
      ProductId = productId,
      EffectiveDate = effectiveDate,
      EndDate = endDate,
      DateChanged = dateChanged,
      ChangedBy = changedBy,
      ListPrice = listPrice,
      PriceChange = priceChange,
      Notes = notes
    };
  }
}
