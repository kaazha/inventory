using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Mapster;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class Get : Endpoint<GetProductByIdRequest, ProductDto>
{
  private readonly IReadRepository<Product> _repository;

  public Get(IReadRepository<Product> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/products/{productId}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetProductByIdRequest request, CancellationToken cancellationToken)
  {
    var specification = new ProductByIdSpecification(request.ProductId);
    var product = await _repository.FirstOrDefaultAsync(specification, cancellationToken);
    if (product == null)
    {
      await SendNotFoundAsync();
      return;
    }

    var dto = product.Adapt<ProductDto>(MapperExtensions.ProductMapperConfig);
    await SendOkAsync(dto);
  }    
}

