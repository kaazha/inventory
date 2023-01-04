using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.ProductPhotoAggregate;

public class ProductPhoto : EntityBase<int>, IAggregateRoot
{
  private ProductPhoto() { }

  public ProductPhoto(int productId, string? thumbNailPhotoFileName, string? largePhotoFileName)
  {
    if (string.IsNullOrEmpty(thumbNailPhotoFileName) && string.IsNullOrEmpty(largePhotoFileName))
      throw new ArgumentException("At least one of Thumbnail or Large photo must be provided!");
    ThumbNailPhotoFileName = thumbNailPhotoFileName;
    LargePhotoFileName = largePhotoFileName;
    ProductId = productId;
  }

  public int ProductId { get; set; }
  public string? ThumbNailPhotoFileName { get; private set; } = default!;
  public string? LargePhotoFileName { get; private set; } = default!;
}

