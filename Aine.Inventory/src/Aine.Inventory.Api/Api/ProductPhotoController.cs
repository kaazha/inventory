using System.ComponentModel.DataAnnotations;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductPhotoAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
      return NotFound($"Product thumbnail photo not found! (File={fileName})");
    }

    var imageType = Path.GetExtension(fileName)[1..];
    return PhysicalFile(Path.GetFullPath(fullPath), contentType: $"image/{imageType}");
  }

  [HttpPut("{productId}")]
  [HttpPost("{productId}")]
  [EnableCors]
  public async Task<IActionResult> UpdateProductPhoto(
    int productId,
    [FromForm] UploadModel model,
    [FromServices] IProductPhotoRepository repository
    )
  {
    var file = model.Photo;
    if (file == null || file.Length == 0 || !file.ContentType.Contains("image", StringComparison.OrdinalIgnoreCase))
    {
      return BadRequest("Invalid or missing product photo/picture!");
    }

    var (fileName, filePath) = ConstructProductPhotoFileName(productId, file.FileName, true);

    using var stream = System.IO.File.Create(filePath);
    await file.CopyToAsync(stream);

    var count = await repository.UpdateProductPhotoAsync(productId, fileName);

    return Ok($"Successfully updated product photo! (Name={fileName}, count={count})");
  }

  private (string FileName, string FullFilePath) ConstructProductPhotoFileName(int productId, string uploadedFileName, bool ensureDirectoryExists)
  {
    var productPhotoLocation = _configuration.GetValue<string>("ProductPhotoLocation");
    var fileName = $"{productId}".PadLeft(6, '0') + Path.GetExtension(uploadedFileName);
    var filePath = Path.Combine(productPhotoLocation, fileName);
    if (ensureDirectoryExists && !Directory.Exists(productPhotoLocation))
    {
      Directory.CreateDirectory(productPhotoLocation);
    }

    return (fileName, filePath);
  }

  public class UploadModel
  {
    public int ProductId { get; set; }
    [Required]
    public IFormFile Photo { get; set; } = default!;
  }

  public static bool IsUrl(string url)
    => !string.IsNullOrEmpty(url) && url.StartsWith("http", StringComparison.OrdinalIgnoreCase);
}
