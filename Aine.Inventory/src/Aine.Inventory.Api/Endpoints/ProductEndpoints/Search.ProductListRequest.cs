using Aine.Inventory.Core.ProductAggregate.Specifications;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class ProductListRequest : IProductSearchParams
{
  public int? ProductId { get; set; }
  public int? CategoryId { get; set; }
  public int? SubCategoryId { get; set; }

  /// <summary> Matches productNumber, Name, description </summary>
  public string? Filter { get; set; }
}

