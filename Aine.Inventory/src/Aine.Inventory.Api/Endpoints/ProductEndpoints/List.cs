using System.Threading;
using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Mapster;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class List : EndpointWithoutRequest<IEnumerable<ProductDto>>
{
  private readonly IReadRepository<Product> _repository;

  public List(IReadRepository<Product> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/products");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var productId = Query<int>("productId", false);
    if (productId > 0)
    {
      await GetProductById(productId, cancellationToken);
      return;
    }

    var specification = new ProductSearchSpecification(new ProductListRequest { ProductId = productId });
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

