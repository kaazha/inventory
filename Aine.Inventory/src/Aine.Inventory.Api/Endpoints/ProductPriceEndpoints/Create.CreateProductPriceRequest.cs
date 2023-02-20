namespace Aine.Inventory.Api.Endpoints.ProductPriceEndpoints;

public class CreateProductPriceRequest
{
  public int ProductId {get;set;}

  public DateTime EffectiveDate { get; set; }

  public DateTime? EndDate { get; set; }

  public double ListPrice { get; set; }

  public string? Notes { get; set; }
}
