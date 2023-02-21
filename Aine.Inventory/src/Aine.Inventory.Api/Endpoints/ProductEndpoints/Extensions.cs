using Aine.Inventory.Core.ProductAggregate;
using Mapster;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public static class MapperExtensions
{
  private static TypeAdapterConfig? _productMapperConfig;

  public static ICollection<ProductDto> Map(this IEnumerable<Product> products)
  {
    return products.Select(p => p.Adapt<ProductDto>(ProductMapperConfig)).ToArray();
  }

  public static TypeAdapterConfig ProductMapperConfig => 
    _productMapperConfig ??= TypeAdapterConfig<Product, ProductDto>
      .NewConfig()      
      .Unflattening(true)
      .PreserveReference(true)
      //.Map(d => d.Inventory, s => s.Inventory)
      //.Ignore(p => p.Inventory)
      .Map(d => d.SubCategoryName, s => s.SubCategory!.Name, s => s.SubCategory != null)
      .Map(d => d.ModelName, s => s.Model!.Name, s => s.Model != null)
      .Map(d => d.CategoryName, s => s.SubCategory!.Category!.Name, s => s.SubCategory != null && s.SubCategory.Category != null)
      .Map(d => d.CategoryId, s => s.SubCategory!.CategoryId, s => s.SubCategory != null)
      .Map(d => d.Description, s => s.Description ?? (s.Model != null ? s.Model.Description : null))      
      .Config;
}
