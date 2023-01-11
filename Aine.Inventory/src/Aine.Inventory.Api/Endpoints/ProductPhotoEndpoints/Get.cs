using Aine.Inventory.Core.ProductPhotoAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.ProductPhotoEndpoints;

public class Get : Endpoint<Get.ProductPhotoRequest>
{
  private readonly IReadRepository<ProductPhoto> _repository;

  public Get(IReadRepository<ProductPhoto> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/products/{productId}/photo/thumbnail",
           "/products/{productId}/photo/large");
    
    AllowAnonymous();
  }

  public override async Task HandleAsync(ProductPhotoRequest request, CancellationToken cancellationToken)
  {
    var productId = request.ProductId; //Route<int>("productId");
    var specification = new ProductPhotoSpecification(productId);
    var photo = await _repository.FirstOrDefaultAsync(specification, cancellationToken);
    if (photo is null)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    var isThumbnail = this.HttpContext.Request.Path.Value?.EndsWith("thumbnail", StringComparison.OrdinalIgnoreCase);
    var fileName = isThumbnail == true ? photo.ThumbNailPhotoFileName : photo.LargePhotoFileName;
    if (string.IsNullOrEmpty(fileName))
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    var productPhotoLocation = Config.GetValue<string>("ProductPhotoLocation");
    if(IsUrl(productPhotoLocation))
    {
      await SendRedirectAsync($"{productPhotoLocation}/{fileName}");
      return;
    }

    var fullPath = Path.Combine(productPhotoLocation, fileName);
    if (!File.Exists(fullPath))
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }
    
    var imageType = Path.GetExtension(fileName)[1..];
    await SendFileAsync(new FileInfo(fullPath),
      contentType: $"image/{imageType}",
      cancellation: cancellationToken
    );
  }

  public static bool IsUrl(string url) 
    => !string.IsNullOrEmpty(url) && url.StartsWith("http", StringComparison.OrdinalIgnoreCase);

  public class ProductPhotoRequest
  {
    public int ProductId { get; set; }
  }
}

