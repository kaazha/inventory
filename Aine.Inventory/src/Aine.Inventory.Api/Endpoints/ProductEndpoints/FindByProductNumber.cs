using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Mapster;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class FindByProductNumber : Endpoint<FindByProductNumberRequest, FindByProductNumberResponse>
{
  private readonly IReadRepository<Product> _repository;

  public FindByProductNumber(IReadRepository<Product> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/products/{@productNumber}/find", x => new {x.ProductNumber});
    AllowAnonymous();
  }

  public override async Task HandleAsync(FindByProductNumberRequest request, CancellationToken cancellationToken)
  {
    var productNumber = request.ProductNumber;  // Route<string>("productNumber");
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