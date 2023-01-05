using Aine.Inventory.Core.ProductPhotoAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aine.Inventory.Api.Api;

[Route("[controller]")]
public class ProductPhotoController : BaseApiController
{
  private readonly IReadRepository<ProductPhoto> _repository;
  private readonly IConfiguration _configuration;

  public ProductPhotoController(IReadRepository<ProductPhoto> repository, IConfiguration configuration)
  {
    _repository = repository;
    _configuration = configuration;
  }

  [HttpGet("{productId}/thumbnail")]
  public async Task<IActionResult> GetThumbnailPhoto(int productId, CancellationToken cancellationToken)
  {
    var specification = new ProductPhotoSpecification(productId);
    var photo = await _repository.FirstOrDefaultAsync(specification, cancellationToken);
    if (photo is null || 
      string.IsNullOrEmpty(photo.ThumbNailPhotoFileName) || 
      photo.ThumbNailPhotoFileName.StartsWith("no_image_"))
    {
     return NotFound();
    }

    var fileName = photo.ThumbNailPhotoFileName;
    var productPhotoLocation = _configuration.GetValue<string>("ProductPhotoLocation");
    if (IsUrl(productPhotoLocation))
    {
      return Redirect($"{productPhotoLocation}/{fileName}");
    }

    var fullPath = Path.Combine(productPhotoLocation, fileName);
    if (!System.IO.File.Exists(fullPath))
    {
      return NotFound();
    }

    var imageType = Path.GetExtension(fileName)[1..];
    return PhysicalFile(fullPath, contentType: $"image/{imageType}");
  }

  public static bool IsUrl(string url)
    => !string.IsNullOrEmpty(url) && url.StartsWith("http", StringComparison.OrdinalIgnoreCase);
}
