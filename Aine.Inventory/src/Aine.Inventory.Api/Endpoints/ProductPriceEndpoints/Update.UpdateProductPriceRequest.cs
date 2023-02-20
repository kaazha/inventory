using Aine.Inventory.Core.Interfaces;

namespace Aine.Inventory.Api.Endpoints.ProductPriceEndpoints;

public class UpdateProductPriceRequest : IProductPrice
{
  public int ProductId {get;set;}

  public DateTime EffectiveDate { get; set; }

  public DateTime? EndDate { get; set; }

  public double ListPrice { get; set; }

  public string? Notes { get; set; }

  public string? ChangedBy { get; set; }

  public int Id { get; set; }

  double? IProductPrice.PriceChange => null;
}
