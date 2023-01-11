using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Mapster;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class Search : Endpoint<ProductListRequest, IEnumerable<ProductDto>>
{
  private readonly IReadRepository<Product> _repository;

  public Search(IReadRepository<Product> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/products/search");
    AllowAnonymous();
  }

  public override async Task HandleAsync(ProductListRequest request, CancellationToken cancellationToken)
  {
    if(request.ProductId > 0)
    {
      await GetProductById(request.ProductId.Value, cancellationToken);
      return;
    }

    var specification = new ProductSearchSpecification(request);
    var products = await _repository.ListAsync(specification, cancellationToken);
    await SendOkAsync(products.Map());
  }

  private async Task GetProductById(int productId, CancellationToken cancellationToken)
  {
    var specification = new ProductByIdSpecification(productId);
    var product = await _repository.FirstOrDefaultAsync(specification, cancellationToken);
    if (product == null)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    var dto = product.Adapt<ProductDto>(MapperExtensions.ProductMapperConfig);
    await SendOkAsync(new[] { dto });
  }
}

