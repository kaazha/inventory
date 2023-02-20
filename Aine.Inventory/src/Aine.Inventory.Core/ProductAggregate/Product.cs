using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductInventoryAggregate;
using Aine.Inventory.Core.ProductModelAggregate;
using Aine.Inventory.Core.ProductPhotoAggregate;
using Aine.Inventory.Core.ProductPriceAggregate;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Guard;
using Aine.Inventory.SharedKernel.Interfaces;
using Mapster;

namespace Aine.Inventory.Core.ProductAggregate;

public class Product : EntityBase<int>, IAggregateRoot, IProduct
{
  private static TypeAdapterConfig? _mappingConfig;

  public string ProductNumber { get; private set; } = default!;
  public string Name { get; private set; } = default!;
  public string? Description { get; private set; }
  public int? SubCategoryId { get; private set; }
  public ProductSubCategory? SubCategory { get; private set; }
  public int? ModelId { get; private set; }
  public ProductModel? Model { get; private set; } 
  public string? Color { get; private set; }
  public string? Size { get; private set; }
  public string? SizeUnit { get; private set; }
  public double? Weight { get; private set; }
  public string? WeightUnit { get; private set; }
  public string? Style { get; private set; }
  public int? ReorderPoint { get; private set; }
  public double? StandardCost { get; private set; }
  public double? ListPrice { get; private set; }
  public bool IsActive { get; private set; } = true;
  public DateTime? ModifiedDate { get; private set; }
  public ProductPhoto? ProductPhoto { get; private set; }
  public ICollection<ProductInventory> Inventory { get; private set; } = default!;
  public ICollection<ProductPrice> Prices { get; private set; } = default!;
  IEnumerable<IInventory>  IProduct.Inventory => Inventory;
  IEnumerable<IProductPrice> IProduct.Prices => Prices;

  public static Product Create(IProduct productDto)
  {
    Validate(productDto);
    var product = new Product(); 
    productDto.Adapt(product, MappingConfig);
    if (product.SubCategoryId == 0) product.SubCategoryId = null;
    if (product.Id == 0) product.IsActive = true;
    if (product.ModelId == 0) product.ModelId = null;

    product.Inventory = new List<ProductInventory>();
    productDto.Inventory?.ForEachItem(inv => product.Inventory.Add(ProductInventory.Create(inv, product.Id)));

    //product.Prices = new List<ProductPrice>();
    //productDto.Prices?.ForEachItem(priceInfo => product.Prices.Add(ProductPrice.Create(priceInfo, product.Id)));

    return product;
  }

  private static void Validate(IProduct product)
  {
    Guard.Against.Null(product, "Product");
    GuardModel.Against.NullOrEmpty(product.ProductNumber, "Product Number can't be empty!");
    GuardModel.Against.NullOrEmpty(product.Name, "Product Name can't be empty!");
    GuardModel.Against.Negative(product.ModelId, "Invalid Product Model");    
    GuardModel.Against.Negative(product.SubCategoryId, "Invalid Product Sub-Category");
    GuardModel.Against.Negative(product.ReorderPoint, "Invalid Product Reorder Point. Reorder Level must be a positive amount.");
    GuardModel.Against.Negative(product.Weight, "Invalid Product Weight. Weight must be a positive value.");
    GuardModel.Against.Negative(product.ListPrice, "Invalid Product List Price. List price should be a positive amount.");
    GuardModel.Against.Negative(product.StandardCost, "Invalid Product Standard Cost. Standard Cost should be a positive amount.");
  }

  private static TypeAdapterConfig MappingConfig => _mappingConfig ??=
    TypeAdapterConfig<IProduct, Product>
      .NewConfig()
      .Ignore(p => p.Inventory)      
      .Config;
}

