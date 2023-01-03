using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.ProductPhotoAggregate;

public class ProductPhoto : EntityBase<int>, IAggregateRoot
{
  //private readonly int? _productId = null;

  private ProductPhoto() { }

  public ProductPhoto(Product product, string? thumbNailPhotoFileName, string? largePhotoFileName)
  {
    if (string.IsNullOrEmpty(thumbNailPhotoFileName) && string.IsNullOrEmpty(largePhotoFileName))
      throw new ArgumentException("At least one of Thumbnail or Large photo must be provided!");
    //Product = product;
    ThumbNailPhotoFileName = thumbNailPhotoFileName;
    LargePhotoFileName = largePhotoFileName;
  }

  public int ProductId { get; set; }
  //public int ProductId 
  //{ 
  //  get => _productId ?? Product?.Id ?? 0; 
  //  private init => _productId = value;
  //}

  //public Product? Product { get; private set; } = default!;
  public string? ThumbNailPhotoFileName { get; private set; } = default!;
  public string? LargePhotoFileName { get; private set; } = default!;
}

