namespace Aine.Inventory.Core.ProductAggregate.Specifications;

public interface IProductSearchParams
{
  int? ProductId { get; set; }
  int? CategoryId { get; set; }
  int? SubCategoryId { get; set; }
  /// <summary> Matches productNumber, Name, description </summary>
  string? Filter { get; set; }
}
