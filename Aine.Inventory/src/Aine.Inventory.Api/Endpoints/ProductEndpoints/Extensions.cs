using Aine.Inventory.Core.ProductAggregate;
using Mapster;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public static class Extensions
{
  private static TypeAdapterConfig? _mapperConfig;

  public static ICollection<ProductDto> Map(this IEnumerable<Product> products)
  {
    _mapperConfig ??= TypeAdapterConfig<Product, ProductDto>
      .NewConfig()
       .Map(d => d.SubCategoryName, s => s.SubCategory!.Name, s => s.SubCategory != null)
      .Map(d => d.ModelName, s => s.Model!.Name, s => s.Model != null)
      .Map(d => d.CategoryName, s => s.SubCategory!.Category!.Name, s => s.SubCategory != null && s.SubCategory.Category != null)
      .Map(d => d.CategoryId, s => s.SubCategory!.CategoryId, s => s.SubCategory != null)
      .Map(d => d.Description, s => s.Model!.Description, s => string.IsNullOrEmpty(s.Description) && s.Model != null)
      .Config;

    return products.Select(p => p.Adapt<ProductDto>(_mapperConfig)).ToArray();
  }
}
