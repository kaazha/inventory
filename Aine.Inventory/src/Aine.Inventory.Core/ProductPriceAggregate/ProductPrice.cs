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

  public void SetEndDate()
  {
    if (EndDate.HasValue)
      throw new InvalidOperationException("Unable to update Price End Date!");
    EndDate = DateTime.UtcNow;
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

  //internal static ProductPrice Create(IProductPrice priceInfo, int productId)
  //{
  //  Guard.Against.Null(priceInfo, nameof(priceInfo));
  //  return Create(
  //    priceInfo.Id,
  //    productId,
  //    priceInfo.EffectiveDate,
  //    priceInfo.EndDate,
  //    priceInfo.ListPrice,
  //    priceInfo.PriceChange,
  //    priceInfo.ChangedBy,

  //    );
  //}
}
