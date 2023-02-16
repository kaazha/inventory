using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Mapster;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class FindByProductNumber : EndpointWithoutRequest<FindByProductNumberResponse>
{
  private readonly IReadRepository<Product> _repository;

  public FindByProductNumber(IReadRepository<Product> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/products/find/{ProductNumber}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var productNumber = Route<string>("ProductNumber");
    var specification = new FindProductByProductNumberSpecification(productNumber!);
    var product = await _repository.FirstOrDefaultAsync(specification, cancellationToken);
    if (product == null || product.Id == null || product.Id == 0)
    {
      await SendNotFoundAsync();
      return;
    }

    var result = new FindByProductNumberResponse
    {
      ProductNumber = productNumber!,
      ProductId = product.Id.Value
    };
    await SendOkAsync(result);
  }
}

public class FindByProductNumberRequest
{
  public string ProductNumber { get; set; } = default!;
}

public class FindByProductNumberResponse
{
  public string ProductNumber { get; set; } = default!;
  public int ProductId { get; set; }
}